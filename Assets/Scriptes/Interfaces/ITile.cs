using System;
using UnityEngine;

public interface ITile
{
    bool IsInThePool { get; set; }
    bool IsHaveFog { get;}
    bool IsHaveSpawner { get; }
    void SetPosition(Vector3 position);
    Vector3 GetPosition();
    Vector3 GetSize();
    void AddFog(Fog fog);
    void AddSpawner(EnemiesSpawner spawner);

    event Action<ITile> CheckPosition;
    event Action<ITile> ReturningToPool;
}
