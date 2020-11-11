using UnityEngine;
using UnityEngine.UI;

public class Skills : MonoBehaviour
{
    [SerializeField] private Text _totalText;
    [SerializeField] private SkillPoints[] _skills;
    [SerializeField] private PlayerScoreCounter _scoreCounter;

    public void ShowTotal()
    {
        _totalText.text = GetTotalValue().ToString("0.##");
    }
    public void ConfirmSkills()
    {
        if (_scoreCounter.IsCanBuy(new Score(0, GetTotalValue())))
        {
            foreach (var skill in _skills)
                SaveDataStorage.SaveSkills(skill.SkillType, skill.CurrentValue);

            ShowTotal();
        }
    }

    public void CancelScills()
    {
        foreach(var skill in _skills)
        {
            skill.SetValue(SaveDataStorage.LoadSkills(skill.SkillType));
        }

        ShowTotal();
    }

    private int GetTotalValue()
    {
        int value = 0;

        foreach (var skill in _skills)
        {
            value += skill.CurrentValue;
        }

        return value;
    }
}
