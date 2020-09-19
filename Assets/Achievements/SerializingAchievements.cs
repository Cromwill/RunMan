using System.IO;
using UnityEngine;

public static class SerializingAchievements
{
    public static readonly string path = Path.Combine(Application.dataPath, "AchievementData.json");
    private static object[] _conditionsAchievement;

    public static void AddRange(object[] achivement)
    {
        _conditionsAchievement = achivement;
    }

    public static void Save()
    {
        string[] data = new string[_conditionsAchievement.Length];

        for (int i = 0; i < data.Length; i++)
        {
            data[i] = JsonUtility.ToJson(_conditionsAchievement[i]);
        }

        File.WriteAllLines(path, data);
    }

    public static void Load()
    {
        string[] data = File.ReadAllLines(path);

        for (int i = 0; i < _conditionsAchievement.Length; i++)
        {
            JsonUtility.FromJsonOverwrite(data[i], _conditionsAchievement[i]);
        }
    }
}