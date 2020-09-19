using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventStorage : MonoBehaviour
{
    private List<AchievementEvent> _events = new List<AchievementEvent>();

    public event UnityAction<List<AchievementEvent>> ActionAdded;

    public void AddAction(AchievementEvent achievementEvent)
    {
        _events.Add(achievementEvent);
        ActionAdded?.Invoke(_events);
    }
}
