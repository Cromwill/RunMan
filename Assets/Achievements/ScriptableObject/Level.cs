using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New AchievementLevel", menuName = "Achievement Level", order = 51)]
public class Level : ScriptableObject
{
    [SerializeField] private Condition[] _conditions;

    public Condition[] Conditions => _conditions;
}
