using UnityEngine;

public class Barrier : MonoBehaviour, IMapElement
{
    public void SetElement(Vector3 position)
    {
        transform.position = position;
    }
}
