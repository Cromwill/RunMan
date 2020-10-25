using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemiesConstructor : MonoBehaviour
{
    [SerializeField] private EnemiesSpawner _spawner;
    [SerializeField] private Vector3 _spawnRect;

    private List<SpawnDotData> _currentSpawners = new List<SpawnDotData>();

    public void GenerateEnemeSpawners(ITile[] tiles, ITile currentTile)
    {
        float tileLenght = currentTile.GetSize().x;
        Vector3 min = currentTile.GetPosition() - new Vector3(tileLenght * _spawnRect.x, 0, 0);
        Vector3 max = currentTile.GetPosition() + new Vector3(tileLenght * _spawnRect.x, 0, tileLenght * _spawnRect.z);

        var enveromentTiles = GetTilesOfRange(tiles, min, max);
        var tilesToGenerate = GetTilesOfRange(enveromentTiles, min + new Vector3(0, 0, 2 * tileLenght), max);

        if (_currentSpawners == null)
        {
            _currentSpawners = new List<SpawnDotData>();
            SetSpawnerOnScene(tilesToGenerate[Random.Range(0, tilesToGenerate.Length)]);
        }
        else
        {
            if (_currentSpawners.Where(spawner => spawner.IsInRange(min, max)).Count() == 0)
            {
                SetSpawnerOnScene(tilesToGenerate[Random.Range(0, tilesToGenerate.Length)]);
            }
        }
    }

    private void SetSpawnerOnScene(ITile tile)
    {
        SpawnDotData spawner = new SpawnDotData(tile);
        spawner.Spawner = Instantiate(_spawner, tile.GetPosition(), Quaternion.identity);
        spawner.SetConnectionWithTile();
        _currentSpawners.Add(spawner);
    }

    private ITile[] GetTilesOfRange(ITile[] array, Vector3 min, Vector3 max)
    {
        return array.Where(tile => tile.GetPosition().x >= min.x
        && tile.GetPosition().x <= max.x
        && tile.GetPosition().z <= max.z
        && tile.GetPosition().z >= min.z).ToArray();
    }
}

public class SpawnDotData
{
    public ITile Tile;
    public EnemiesSpawner Spawner;

    public SpawnDotData(ITile tile)
    {
        Tile = tile;
    }

    public bool IsInRange(Vector3 min, Vector3 max)
    {
        Vector3 position = Tile.GetPosition();

        return position.x >= min.x && position.x <= max.x && position.z <= max.z && position.z >= min.z;
    }

    public void SetConnectionWithTile()
    {
        if(Tile != null && Spawner != null)
        {
            Tile.AddSpawner(Spawner);
        }
    }
}
