using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillPoints : MonoBehaviour
{
    [SerializeField] private ScriptableObject SkillData;
    [SerializeField] private Slider _slider;

    public int CurrentValue { get ; private set ; }

    public void ValueChange()
    {
        Debug.Log(_slider.value);
        CurrentValue = (int)_slider.value;
    }
}
