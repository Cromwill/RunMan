using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Condition : ScriptableObject
{
    [SerializeField] private float _neededValue;

    public float NeededValue => _neededValue;

    public abstract bool Met(List<AchievementEvent> events);

    public abstract bool IsActiveCondition();
}