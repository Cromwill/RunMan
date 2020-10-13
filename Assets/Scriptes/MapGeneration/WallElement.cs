using UnityEngine;
using System.Collections;

public class WallElement : MonoBehaviour, IMapElement
{
    public void SetElement(Vector3 position) => transform.position = position;
}
