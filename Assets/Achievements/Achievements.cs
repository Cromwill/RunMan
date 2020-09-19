using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Achievements : MonoBehaviour
{
    [SerializeField] private EventStorage _eventStorage;
    [SerializeField] private AchievementData[] _achievements;

    public event UnityAction<AchievementData> AchievementReached;

    private void Start()
    {
        SerializingAchievements.AddRange(_achievements);

        if (File.Exists(SerializingAchievements.path))
        {
            SerializingAchievements.Load();
        }
    }

    private void OnEnable()
    {
        _eventStorage.ActionAdded += OnActionAdded;
    }

    private void OnDisable()
    {
        _eventStorage.ActionAdded -= OnActionAdded;
    }

    private void OnActionAdded(List<AchievementEvent> events)
    {
        TryReaсhAchievement(events);

        SerializingAchievements.Save();
    }

    private void TryReaсhAchievement(List<AchievementEvent> events)
    {
        foreach (var achievement in _achievements)
        {
            if (achievement.Levels[achievement.CurrentLevel].Conditions.Length == 0)
                continue;

            bool achievementReached = true;

            for (int i = 0; i < achievement.Levels[achievement.CurrentLevel].Conditions.Length; i++)
            {
                if (achievement.Levels[achievement.CurrentLevel].Conditions[i].Met(events) == false)
                {
                    achievementReached = false;
                }
            }

            if (achievementReached)
            {
                achievement.AddLevel();
                AchievementReached?.Invoke(achievement);
            }
        }
    }
}