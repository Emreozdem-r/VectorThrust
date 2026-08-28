using UnityEngine;
using TMPro;

public class EndlessScoreManager : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    [SerializeField] TMP_Text currentScoreText;
    [SerializeField] TMP_Text highScoreText;

    float startX;
    int currentScore = 0;
    int highScore = 0;

    void Start()
    {
        if (playerTransform != null)
        {
            startX = playerTransform.position.x;
        }

        highScore = PlayerPrefs.GetInt("EndlessHighScore", 0);
        UpdateHighScoreUI();
    }

    void Update()
    {
        if (playerTransform == null) return;

        // Ileriye dogru alinan mesafe (X ekseni)
        float distanceTraveled = playerTransform.position.x - startX;
        if (distanceTraveled > currentScore)
        {
            currentScore = Mathf.FloorToInt(distanceTraveled);
            if (currentScoreText != null)
            {
                currentScoreText.text = $"Score: {currentScore}m";
            }

            if (currentScore > highScore)
            {
                highScore = currentScore;
                PlayerPrefs.SetInt("EndlessHighScore", highScore);
                PlayerPrefs.Save();
                UpdateHighScoreUI();
            }
        }
    }

    void UpdateHighScoreUI()
    {
        if (highScoreText != null)
        {
            highScoreText.text = $"Best: {highScore}m";
        }
    }
}