using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

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

        Vector3 minVector = new Vector3(0, tiles[0].GetPosition().y, 0);
        Vector3 maxVector = new Vector3(0, tiles[0].GetPosition().y, 0);

        foreach (var tile in tiles)
        {
            Vector3 position = tile.GetPosition();

            if (position.x < minVector.x)
                minVector.x = position.x;
            if (position.x > maxVector.x)
                maxVector.x = position.x;
            if (position.z < minVector.z)
                minVector.z = position.z;
            if (position.z > maxVector.z)
                maxVector.z = position.z;
        }

        ITile minTile = tiles.Where(a => a.GetPosition().x == minVector.x && a.GetPosition().z == minVector.z).First();
        ITile maxTile = tiles.Where(a => a.GetPosition().x == maxVector.x && a.GetPosition().z == maxVector.z).First();

        Debug.Log("MinTile - " + minTile.GetPosition());
        Debug.Log("MaxTile - " + maxTile.GetPosition());


        if (_currentSpawners.Where(spawner => spawner.IsInRange(min, max)).Count() == 0)
        {
            SetSpawnerOnScene(tilesToGenerate[Random.Range(0, tilesToGenerate.Length)]);
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
        if (Tile != null && Spawner != null)
        {
            Tile.AddSpawner(Spawner);
        }
    }
}
