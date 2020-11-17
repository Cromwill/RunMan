using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Skill", menuName = "SkillData")]
public class SkillData : ScriptableObject
{
    [SerializeField] private int _basePrice;
    [SerializeField] private float _incrementStep;
    [SerializeField] private SkillType _type;

    [SerializeField] private string _saveKey;

    public string skillKey => _saveKey;

    public int GetPrice(int count)
    {
        return (int)(Mathf.Pow(_incrementStep, count) - 1) * _basePrice;
    }
}
