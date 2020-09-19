using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New AchievementData", menuName = "Achievement Condition/Distance Covered", order = 51)]
public class MoveCondition : Condition
{
    private float _distanceСovered;

    public override bool IsActiveCondition()
    {
        return _distanceСovered >= NeededValue;
    }

    public override bool Met(List<AchievementEvent> events)
    {
        foreach (AchievementEvent achievementEvent in events)
        {
            if (achievementEvent is MoveEvent moveEvent)
            {
                _distanceСovered += moveEvent.DistanceCovered;

                events.Remove(achievementEvent);

                return _distanceСovered > NeededValue;
            }
        }

        return false;
    }
}
