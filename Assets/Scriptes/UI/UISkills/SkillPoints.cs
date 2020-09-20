using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillPoints : MonoBehaviour
{
    [SerializeField] private ScriptableObject SkillData;
    [SerializeField] private Slider _slider;

    private Skills _skills;

    private void OnEnable()
    {
        _slider.onValueChanged.AddListener(ValueChange);
        if (_skills == null)
            _skills = GetComponentInParent<Skills>();
    }

    public int CurrentValue { get ; private set ; }

    public void ValueChange(float value)
    {
        Debug.Log(_slider.value);
        CurrentValue = (int)_slider.value;
        _skills.ShowTotal();
    }
}
