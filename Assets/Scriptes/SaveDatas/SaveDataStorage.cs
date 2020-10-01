using Boo.Lang;
using System;
using UnityEngine;

public static class SaveDataStorage
{
    public static void SaveCurrentRunners(IBuyableObject avatar)
    {
        PlayerPrefs.SetInt("Type_" + avatar.Type + "_Id_" + avatar.Id, avatar.Id);
        PlayerPrefs.SetInt("CurrentAvatarId", avatar.Id);
        PlayerPrefs.Save();
    }

    public static List<int> LoadOpenedRunnersIds(int maxValue)
    {
        List<int> avatarIds = new List<int>();

        for (int i = 0; i <= maxValue; i++)
        {
            if (PlayerPrefs.HasKey("Type_Avatar_Id_" + i))
            {
                avatarIds.Add(PlayerPrefs.GetInt("Type_Avatar_Id_" + i));
            }
        }

        return avatarIds;
    }

    public static int LoadCurrentRunnersId()
    {
        return PlayerPrefs.GetInt("CurrentAvatarId");
    }

    public static void SaveSkills(SkillType skillType, int value)
    {
        PlayerPrefs.SetInt("SavedSkill_" + skillType.ToString(), value);
        PlayerPrefs.Save();
    }

    public static int LoadSkills(SkillType skillType)
    {
        return PlayerPrefs.GetInt("SavedSkill_" + skillType.ToString());
    }

    public static void SaveScore(Score score)
    {
        PlayerPrefs.SetInt("Money", score.Money);
        PlayerPrefs.SetInt("Coins", score.Coins);
        PlayerPrefs.Save();
    }

    public static Score LoadScore()
    {
        return new Score(PlayerPrefs.GetInt("Money"), PlayerPrefs.GetInt("Coins"));
    }
}

public class Score
{
    public int Money { get; private set; }
    public int Coins { get; private set; }

    public Score(int money, int coins)
    {
        Money = money;
        Coins = coins;
    }
}

public enum SkillType
{
    RunningSpeed,
    SwingSpeed,
    RateOfFire,
    Damage
}

