using UnityEngine;
using UnityEngine.UI;

public class Skills : MonoBehaviour
{
    [SerializeField] private Text _totalText;
    [SerializeField] private SkillPoints[] _skills;

    public void ShowTotal()
    {
        int value = 0;

        foreach (var skill in _skills)
        {
            value += skill.CurrentValue;
        }

        _totalText.text = value.ToString("0.##");
    }
    public void ConfirmSkills()
    {
        foreach (var skill in _skills)
            SaveDataStorage.SaveSkills(skill.SkillType, skill.CurrentValue);

        ShowTotal();
    }

    public void CancelScills()
    {
        foreach(var skill in _skills)
        {
            skill.SetValue(SaveDataStorage.LoadSkills(skill.SkillType));
        }

        ShowTotal();
    }
}
