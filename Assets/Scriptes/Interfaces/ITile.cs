using System;
using UnityEngine;

public interface ITile
{
    void SetPosition(Vector3 position);
    Vector3 GetPosition();
    Vector3 GetSize();

    event Action<ITile> CheckPosition;
}
