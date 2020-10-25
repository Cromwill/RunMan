using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapElementPool : MonoBehaviour
{
    [SerializeField] private List<MapElement> _nonDestroyObjects;
    [SerializeField] private List<MapElement> _destroyObjects;
    [SerializeField] private List<TileGeneration> _tiles;
    [SerializeField] private int _tilesCount;

    private ITile[] _tilePool;

    private void Awake()
    {
        Time.timeScale = 1;
        GeneratePool();
    }
    public IMapElement GetNonDestroyObject(Transform parent)
    {
        IMapElement element = Instantiate(_nonDestroyObjects[Random.Range(0, _nonDestroyObjects.Count)], parent);
        return element;
    }

    public IMapElement GetNonDestroyObject() => _nonDestroyObjects[Random.Range(0, _nonDestroyObjects.Count)];

    public IMapElement GetDestroyObject() => _destroyObjects[Random.Range(0, _destroyObjects.Count)];

    public ITile GetTile()
    {
        var tilesThatAtPool = _tilePool.Where(t => t.IsInThePool == true).ToArray();
        ITile tile;
        try
        {
            tile = tilesThatAtPool[Random.Range(0, tilesThatAtPool.Length)];
        }
        catch (IndexOutOfRangeException)
        {
            Debug.Log("");
            return null;
        }

        tile.IsInThePool = false;
        return tile;
    }

    public void ReturnToPool(ITile tile)
    {
        Vector3 position = transform.position;

        for (int i = 0; i < _tilePool.Length; i++)
        {
            if (_tilePool[i] == tile)
            {
                position = new Vector3(position.x, position.y - i, position.z);
                _tilePool[i].SetPosition(position);
                _tilePool[i].IsInThePool = true;
            }
        }
    }

    private void GeneratePool()
    {
        _tilePool = new ITile[_tilesCount];
        Vector3 position = transform.position;

        for (int i = 0; i < _tilesCount; i++)
        {
            _tilePool[i] = Instantiate(_tiles[Random.Range(0, _tiles.Count)]);
            _tilePool[i].SetPosition(new Vector3(position.x, position.y - i, position.z));
        }
    }
}
