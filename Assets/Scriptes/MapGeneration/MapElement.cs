using UnityEngine;
using System.Collections;

public class MapElement : MonoBehaviour, IMapElement
{
    public void SetElement(Vector3 position) => transform.position = position;

}
