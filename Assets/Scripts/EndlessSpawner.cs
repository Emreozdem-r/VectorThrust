using System.Collections.Generic;
using UnityEngine;

public class EndlessSpawner : MonoBehaviour
{
    [Header("Referanslar")]
    [SerializeField] private Transform player;

    [Header("Parça Havuzlarý")]
    [Tooltip("Oyun baþýnda gelecek boþ/güvenli arka plan parçalarý")]
    [SerializeField] private GameObject[] safePrefabs;

    [Tooltip("Belli bir mesafeden sonra devreye girecek engelli parçalar")]
    [SerializeField] private GameObject[] obstaclePrefabs;

    [Header("Mesafe & Doðma Ayarlarý")]
    [SerializeField] private float sectionWidth = 60f;          // Parçanýn X uzunluðu
    [SerializeField] private float spawnDistanceAhead = 100f;    // Kameranýn görüþ menzilinin çok ilerisinde doðsun
    [SerializeField] private float cleanupDistanceBehind = 70f;  // Arkada kalan parçayý silme mesafesi
    [SerializeField] private int initialSections = 3;           // Peþin dizilecek parça sayýsý
    [SerializeField] private int safeSectionLimit = 2;          // Ýlk kaç parçada engel olmasýn?

    private float nextSpawnX = 0f;
    private int spawnedCount = 0;
    private List<GameObject> activeSections = new List<GameObject>();

    private void Start()
    {
        // Pop-in sorununu önlemek için oyun baþýnda roketin önüne peþin parçalar dizilir
        for (int i = 0; i < initialSections; i++)
        {
            SpawnSection();
        }
    }

    private void Update()
    {
        if (player == null) return;

        // Roket yaklaþmadan çok önce (100 birim ileride) yeni parça çaðrýlýr
        if (player.position.x + spawnDistanceAhead > nextSpawnX)
        {
            SpawnSection();
        }

        CleanupOldSections();
    }

    private void SpawnSection()
    {
        GameObject chosenPrefab;

        // Roket henüz baþlangýç evresindeyse güvenli parça ver
        if (spawnedCount < safeSectionLimit || obstaclePrefabs.Length == 0)
        {
            int index = Random.Range(0, safePrefabs.Length);
            chosenPrefab = safePrefabs[index];
        }
        else
        {
            // Roket ilerledikçe artýk engelli havuzundan rastgele parça seç
            int index = Random.Range(0, obstaclePrefabs.Length);
            chosenPrefab = obstaclePrefabs[index];
        }

        Vector3 spawnPos = new Vector3(nextSpawnX, 0, 0);
        GameObject newSection = Instantiate(chosenPrefab, spawnPos, Quaternion.identity);
        activeSections.Add(newSection);

        nextSpawnX += sectionWidth;
        spawnedCount++;
    }

    private void CleanupOldSections()
    {
        if (activeSections.Count > 0 && activeSections[0] != null)
        {
            // Arkada kalan parça görüþ alanýndan çýkýnca silinir (bellek þiþmesini önler)
            if (player.position.x - activeSections[0].transform.position.x > cleanupDistanceBehind)
            {
                Destroy(activeSections[0]);
                activeSections.RemoveAt(0);
            }
        }
    }
}