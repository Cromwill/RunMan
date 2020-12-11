﻿using System;
using UnityEngine;

public class PlayerDamager : MonoBehaviour, IPlayerComponent
{
    [SerializeField] private int _bulletCount;
    [SerializeField] private ArmorInfo _armorViewer;
    [SerializeField] private float _shootSpeed;
    [SerializeField] private float _defaultDamage;
    [SerializeField] private BoosterType _type;

    private PlayerDamageZone _damageZone;

    public event Action BulletsRunOut;

    public BoosterType BoosterType => _type;

    private void Start()
    {
        _damageZone = GetComponentInChildren<PlayerDamageZone>();
        _damageZone.FindedEnemies += Shoot;
        if (_armorViewer != null)
            _armorViewer.Show(_bulletCount);
    }

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
        switch (skill.skillKey)
        {
            case "Damage":
                _defaultDamage *= (skill.Multiplier * count);
                break;
            case "RateOfFire":
                _shootSpeed *= (skill.Multiplier * count);
                break;
        }
    }

    private void UsedBooster(Booster booster)
    {
        switch (booster.GetItemName)
        {
            case "Damage":
                _defaultDamage += booster.Value;
                break;
        }
    }

    private void Shoot(Enemy enemy)
    {
        if (_bulletCount > 0)
        {
            Debug.Log("damage");
            _bulletCount--;
            _armorViewer.Show(_bulletCount);
            enemy.AddDamage(_defaultDamage);
        }
    }
}
