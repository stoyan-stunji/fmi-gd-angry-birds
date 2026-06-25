using UnityEngine;

public static class ProgressManager
{
    private const string HighestUnlockedKey = "HighestUnlockedLevel";

    public static int HighestUnlockedLevel
    {
        get
        {
            return PlayerPrefs.GetInt(HighestUnlockedKey, 1);
        }
    }

    public static void UnlockLevel(int levelNumber)
    {
        if (levelNumber > HighestUnlockedLevel)
        {
            PlayerPrefs.SetInt(HighestUnlockedKey, levelNumber);
            PlayerPrefs.Save();
        }
    }

    public static bool IsLevelUnlocked(int levelNumber)
    {
        return levelNumber <= HighestUnlockedLevel;
    }

    public static void ResetProgress()
    {
        PlayerPrefs.DeleteKey(HighestUnlockedKey);
    }

    public static bool IsCompleted(int level)
    {
        return PlayerPrefs.GetInt($"Level{level}Completed", 0) == 1;
    }

    public static void CompleteLevel(int level)
    {
        PlayerPrefs.SetInt($"Level{level}Completed", 1);
    }
}