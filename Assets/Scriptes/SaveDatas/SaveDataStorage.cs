using JetBrains.Annotations;
using System;
using System.Collections.Generic;
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

    public static void AddScore(Score score)
    {
        Score oldScore = LoadScore();
        Score newScore = new Score(oldScore.Money + score.Money, oldScore.Coins + score.Coins);
        SaveScore(newScore);
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

public class Score : IEquatable<Score>, IComparable<Score>
{
    public int Money { get; private set; }
    public int Coins { get; private set; }

    public Score(int money, int coins)
    {
        Money = money;
        Coins = coins;
    }

    public override int GetHashCode() => (Money, Coins).GetHashCode();

    public bool Equals(Score other)
    {
        if (other == null)
            return false;
        if (Money == other.Money && Coins == other.Coins)
            return true;
        else
            return false;
    }

    public override bool Equals(object obj)
    {
        if (obj == null) return false;

        Score score = obj as Score;
        if (score == null)
            return false;
        else
            return Equals(score);
    }

    public int CompareTo(Score other)
    {
        if (other == null) return 1;
        int moneyResult = Money.CompareTo(other.Money);
        int coinsResult = Coins.CompareTo(other.Coins);

        return Mathf.Min(moneyResult, coinsResult);
    }

    public static bool operator ==(Score score1, Score score2)
    {
        if (((object)score1) == null || ((object)score2) == null)
            return System.Object.Equals(score1, score2);
        return score1.Equals(score2);
    }

    public static bool operator !=(Score score1, Score score2)
    {
        if (((object)score1) == null || ((object)score2) == null)
            return !System.Object.Equals(score1, score2);

        return !(score1.Equals(score2));
    }

    public static bool operator >(Score score1, Score score2)
    {
        return score1.Money.CompareTo(score2.Money) == 1 && score1.Coins.CompareTo(score2.Coins) == 1;
    }

    public static bool operator <(Score score1, Score score2)
    {
        return score1.Money.CompareTo(score2.Money) == -1 && score1.Coins.CompareTo(score2.Coins) == -1;
    }
    public static bool operator >=(Score score1, Score score2)
    {
        return score1.Money.CompareTo(score2.Money) >= 0 && score1.Coins.CompareTo(score2.Coins) >= 0;
    }
    public static bool operator <=(Score score1, Score score2)
    {
        return score1.Money.CompareTo(score2.Money) <= 0 && score1.Coins.CompareTo(score2.Coins) <= 0;
    }

    public static Score operator +(Score score) => score;
    public static Score operator -(Score score) => new Score(-score.Money, -score.Coins);
    public static Score operator +(Score score1, Score score2) => new Score(score1.Money + score2.Money, score1.Coins + score2.Coins);
    public static Score operator -(Score score1, Score score2) => new Score(score1.Money - score2.Money, score1.Coins - score2.Coins);
}

public enum SkillType
{
    RunningSpeed,
    SwingSpeed,
    RateOfFire,
    Damage
}

