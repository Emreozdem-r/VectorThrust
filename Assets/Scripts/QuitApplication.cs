using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class QuitApplication : MonoBehaviour
{
    void Update()
    {
        if (Keyboard.current == null) return;

        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            // Eðer Endless sahnesindeysek duraklatma menüsünü tetikle
            if (SceneManager.GetActiveScene().name == "Endless")
            {
                EndlessGameManager manager = FindFirstObjectByType<EndlessGameManager>();
                if (manager != null)
                {
                    manager.TogglePause();
                }
            }
            // Diðer tüm normal görevlerdeysek direkt ana menüye dön
            else
            {
                ReturnToMenu();
            }
        }
    }

    public void ReturnToMenu()
    {
        Time.timeScale = 1f; // Menüye dönerken zamanýn akmasýný saðla
        SceneManager.LoadScene("MainMenu");
    }
}