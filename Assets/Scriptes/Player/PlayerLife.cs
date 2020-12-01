using UnityEngine;
using System.Collections;

public class PlayerLife : MonoBehaviour, IPlayerComponent
{
    [SerializeField] private float _defaultLife;
    [SerializeField] private float _defualtArmor;
    [SerializeField] private float _safeTime;
    [SerializeField] private BoosterType _type;

    private bool _isSafeTime;

    public BoosterType BoosterType => _type;

    public void Initialization(params Booster[] boosters)
    {
        if (boosters != null)
        {
            foreach (Booster booster in boosters)
                UsedBooster(booster);
        }
    }

    public void UsedSkill(SkillData skill, int count)
    {
    }

    private void UsedBooster(Booster booster)
    {
        switch(booster.GetItemName)
        {
            case "armor":
                _defualtArmor += booster.Value;
                break;
            case "life":
                _defaultLife += booster.Value;
                break;
        }
    }

}
