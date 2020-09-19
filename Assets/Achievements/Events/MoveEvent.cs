using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEvent : AchievementEvent
{
    private float _distanceCovered;

    public float DistanceCovered => _distanceCovered;

    public MoveEvent(float distanceCovered)
    {
        _distanceCovered = distanceCovered;
    }
}
