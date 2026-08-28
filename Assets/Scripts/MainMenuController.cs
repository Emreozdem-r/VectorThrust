using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] GameObject levelPanel;

    [Header("Locked Buttons")]
    [SerializeField] Button downwardButton;
    [SerializeField] Button speedTrailButton;
    [SerializeField] Button finalButton;

    [Header("Sound")]
    [SerializeField] TMP_Text soundButtonLabel;
    [SerializeField] GameObject mainPanel;

    bool soundOn;

    void Start()
    {

        levelPanel.SetActive(false);

        soundOn = PlayerPrefs.GetInt("SoundOn", 1) == 1;
        ApplySound();
        RefreshLockedButtons();
    }

    public void Play()
    {
        SceneManager.LoadScene(GameProgress.HighestUnlockedLevel);
    }

    public void ShowLevelSelect()
    {
        mainPanel.SetActive(false);
        levelPanel.SetActive(true);
    }

    public void HideLevelSelect()
    {
        levelPanel.SetActive(false);
        mainPanel.SetActive(true);
    }

    public void LoadFirst() => SceneManager.LoadScene("First");
    public void LoadDownward() => SceneManager.LoadScene("Downward");
    public void LoadSpeedTrail() => SceneManager.LoadScene("Speed Trail");
    public void LoadFinal() => SceneManager.LoadScene("Final");
    public void LoadEndless() => SceneManager.LoadScene("Endless");

    public void ToggleSound()
    {
        soundOn = !soundOn;
        PlayerPrefs.SetInt("SoundOn", soundOn ? 1 : 0);
        PlayerPrefs.Save();
        ApplySound();
    }

    void ApplySound()
    {
        AudioListener.volume = soundOn ? 1f : 0f;

        if (soundButtonLabel != null)
        {
            soundButtonLabel.text = soundOn ? "Audio : On" : "Audio : Off";
        }
    }

    void RefreshLockedButtons()
    {
        downwardButton.interactable = GameProgress.HighestUnlockedLevel >= 2;
        speedTrailButton.interactable = GameProgress.HighestUnlockedLevel >= 3;
        finalButton.interactable = GameProgress.HighestUnlockedLevel >= 4;
    }
    public void QuitGame()
    {
        UnityEngine.Application.Quit();
#if UNITY_EDITOR
    UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}