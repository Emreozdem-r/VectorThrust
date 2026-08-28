using UnityEngine;

public static class GameProgress
{
    const string HighestUnlockedKey = "HighestUnlockedLevel";
    const string EndlessUnlockedKey = "EndlessUnlocked";

    public static int HighestUnlockedLevel
    {
        get { return PlayerPrefs.GetInt(HighestUnlockedKey, 1); }
    }

    public static bool EndlessUnlocked
    {
        get { return PlayerPrefs.GetInt(EndlessUnlockedKey, 0) == 1; }
    }

    public static void CompleteLevel(int completedLevel)
    {
        if (completedLevel < 4)
        {
            int nextLevel = completedLevel + 1;

            if (nextLevel > HighestUnlockedLevel)
            {
                PlayerPrefs.SetInt(HighestUnlockedKey, nextLevel);
            }
        }
        else
        {
            PlayerPrefs.SetInt(EndlessUnlockedKey, 1);
        }

        PlayerPrefs.Save();
    }
}