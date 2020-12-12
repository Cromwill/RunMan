using UnityEngine;

public static class AchievementDataStorage
{
    public static void AddAchievement(Achievement achievement, float value)
    {
        if (achievement.ID == 1)
        { }

        float savedValue = PlayerPrefs.GetFloat("Achievement_ID_" + achievement.ID);

        if (achievement.IsRecord && savedValue < value)
            SaveAchievementData(achievement, value);
        else if (!achievement.IsRecord)
            SaveAchievementData(achievement, savedValue + value);
    }

    public static float GetAchievementValue(Achievement achievement)
    {
        return PlayerPrefs.GetFloat("Achievement_ID_" + achievement.ID);
    }

    private static void SaveAchievementData(Achievement achievement, float value)
    {
        PlayerPrefs.SetFloat("Achievement_ID_" + achievement.ID, value);
        PlayerPrefs.Save();
    }
}

