using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EndlessGameManager : MonoBehaviour
{
    [Header("Referanslar")]
    [SerializeField] private Transform player;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private GameObject pausePanel;

    private float startX;
    private int currentScore = 0;
    private int highScore = 0;
    private bool isPaused = false;

    private void Start()
    {
        // Sahne açýldýðýnda zamanýn aktýðýndan emin ol
        Time.timeScale = 1f;

        if (player != null)
        {
            startX = player.position.x;
        }

        // Hafýzaya kaydedilmiþ en yüksek rekoru çek
        highScore = PlayerPrefs.GetInt("EndlessHighScore", 0);
        UpdateHighScoreUI();

        if (pausePanel != null)
        {
            pausePanel.SetActive(false); // Baþlangýçta pause menüsü kapalý
        }
    }

    private void Update()
    {
        // 1. Puan / Ýlerleme Hesabý
        if (player != null)
        {
            float distance = player.position.x - startX;
            if (distance > currentScore)
            {
                currentScore = Mathf.FloorToInt(distance);
                if (scoreText != null)
                {
                    scoreText.text = $"Score: {currentScore}m";
                }

                // Anlýk rekor kýrýlýrsa rekoru güncelle ve kaydet
                if (currentScore > highScore)
                {
                    highScore = currentScore;
                    PlayerPrefs.SetInt("EndlessHighScore", highScore);
                    PlayerPrefs.Save();
                    UpdateHighScoreUI();
                }
            }
        }
    }

    private void UpdateHighScoreUI()
    {
        if (highScoreText != null)
        {
            highScoreText.text = $"High Score: {highScore}m";
        }
    }

    // --- Pause Menüsü Buton Fonksiyonlarý ---
    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f; // Fizik ve oyun zamanýný dondur
        if (pausePanel != null) pausePanel.SetActive(true);
    }
public void TogglePause()
{
    if (isPaused)
    {
        ResumeGame();
    }
    else
    {
        PauseGame();
    }
}

public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f; // Zamaný normale döndür
        if (pausePanel != null) pausePanel.SetActive(false);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f; // Menüye dönerken zamaný açmayý unutma!
        SceneManager.LoadScene("MainMenu");
    }
}