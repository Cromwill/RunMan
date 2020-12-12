using System;
using System.Collections;
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
    [SerializeField] private int _startTilesCount;

    private ITile[] _tilePool;

    private void Awake()
    {
        Time.timeScale = 1;
        GeneratePool(_tilesCount);
    }
    public IMapElement GetNonDestroyObject(Transform parent)
    {
        IMapElement element = Instantiate(_nonDestroyObjects[Random.Range(0, _nonDestroyObjects.Count)], parent);
        element.RandomRotate();
        return element;
    }

    public IMapElement GetNonDestroyObject()
    {
        IMapElement element = _nonDestroyObjects[Random.Range(0, _nonDestroyObjects.Count)];
        element.RandomRotate();
        return element;
    }
    public IMapElement GetDestroyObject()
    {
        IMapElement element = _destroyObjects[Random.Range(0, _destroyObjects.Count)];
        element.RandomRotate();
        return element;
    }

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

    private void GeneratePool(int count)
    {
        _tilePool = new ITile[count];
        Vector3 position = transform.position;

        for (int i = 0; i < count; i++)
        {
            _tilePool[i] = Instantiate(_tiles[Random.Range(0, _tiles.Count)]);
            _tilePool[i].SetPosition(new Vector3(position.x, position.y - i, position.z));
        }
    }

    private IEnumerator GenerateTileByStep()
    {
        while (true)
        {
            yield return new WaitForSeconds(20);
        }
    }
}
