using UnityEngine;

public class InfiniteBackground : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float backgroundWidth = 60f; // Arka plan bloðunun X geniþliði

    void Update()
    {
        if (player == null) return;

        // Roket bu arka plan parçasýný tamamen geçtiðinde
        if (player.position.x - transform.position.x > backgroundWidth)
        {
            // Bu parçayý diðer parçanýn hemen önüne (2 katý ileriye) ýþýnla
            transform.position += new Vector3(backgroundWidth * 2f, 0, 0);
        }
    }
}