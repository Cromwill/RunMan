using System;
using UnityEngine;

public interface ITile
{
    bool IsInThePool { get; set; }
    bool IsHaveFog { get;}
    void SetPosition(Vector3 position);
    Vector3 GetPosition();
    Vector3 GetSize();
    void AddFog(Fog fog);

    event Action<ITile> CheckPosition;
}
