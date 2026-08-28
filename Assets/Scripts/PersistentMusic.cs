using UnityEngine;

public class PersistentMusic : MonoBehaviour
{
    [SerializeField] private string musicID;

    private static PersistentMusic currentMusic;

    private void Awake()
    {
        if (currentMusic != null)
        {
            // 1. Ayný müzik çalýyorsa (Örn: Sonsuz modda yandýn ve sahne yeniden baþladý)
            if (currentMusic.musicID == this.musicID)
            {
                Destroy(gameObject); // Yeni doðan objeyi sil, eski müzik kesilmeden devam etsin
                return;
            }
            // 2. Farklý bir müziðe geçildiyse (Örn: Endless -> MainMenu)
            else
            {
                Destroy(currentMusic.gameObject); // Eski çalan müziði öldür
            }
        }

        currentMusic = this;
        DontDestroyOnLoad(gameObject);
    }
}