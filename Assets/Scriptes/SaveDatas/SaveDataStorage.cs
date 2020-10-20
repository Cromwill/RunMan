using Boo.Lang;
using System;
using UnityEngine;

public static class SaveDataStorage
{
    public static void SaveBuyableObject(IBuyableObject buyable)
    {
        if (buyable.Type == "avatar")
        {
            PlayerPrefs.SetInt("Type_" + buyable.Type + "_Id_" + buyable.Id, buyable.Id);
            PlayerPrefs.SetInt("CurrentAvatarId", buyable.Id);
        }
        else
        {
            SaveItem(buyable as IItem, true);
        }
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

    public static void SaveItem(IItem item, bool isItemHave)
    {
        PlayerPrefs.SetString("Haveable_item_" + item.GetItemName, isItemHave.ToString());
        PlayerPrefs.Save();
    }

    public static bool ItemContain(IItem item)
    {
        if (PlayerPrefs.HasKey("Haveable_item_" + item.GetItemName))
        {
            return PlayerPrefs.GetString("Haveable_item_" + item.GetItemName) == true.ToString();
        }
        else
            return false;
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

