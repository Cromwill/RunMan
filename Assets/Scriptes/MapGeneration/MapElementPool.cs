using System.Collections.Generic;
using UnityEngine;

public class MapElementPool : MonoBehaviour
{
    [SerializeField] private List<MapElement> _nonDestroyObjects;
    [SerializeField] private List<MapElement> _destroyObjects;

    public IMapElement GetNonDestroyObject(Transform parent)
    {
        IMapElement element = Instantiate(_nonDestroyObjects[Random.Range(0, _nonDestroyObjects.Count)], parent);
        return element;
    }

    public IMapElement GetNonDestroyObject() => _nonDestroyObjects[Random.Range(0, _nonDestroyObjects.Count)];

    public IMapElement GetDestroyObject() => _destroyObjects[Random.Range(0, _destroyObjects.Count)];
}
