using System;
using UnityEngine;

public class MonsterDieEvent : AchievementEvent
{
    public Type Sender { get; private set; }

    public MonsterDieEvent(Type sender)
    {
        Sender = sender;
    }
}
