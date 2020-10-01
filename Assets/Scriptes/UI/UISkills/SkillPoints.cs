using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillPoints : MonoBehaviour
{
    [SerializeField] private ScriptableObject SkillData;
    [SerializeField] private Slider _slider;
    [SerializeField] private SkillType _skillType;

    private Skills _skills;

    public int CurrentValue { get; private set; }
    public SkillType SkillType => _skillType;

    private void OnEnable()
    {
        _slider.onValueChanged.AddListener(ValueChange);
        if (_skills == null)
            _skills = GetComponentInParent<Skills>();

        SetValue(SaveDataStorage.LoadSkills(_skillType));
    }

    public void ValueChange(float value)
    {
        CurrentValue = (int)_slider.value;
        _skills.ShowTotal();
    }

    public void SetValue(int value)
    {
        _slider.value = value;
        CurrentValue = value;
        _skills.ShowTotal();
    }
}
