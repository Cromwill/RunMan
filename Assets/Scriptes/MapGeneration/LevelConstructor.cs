using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(FogConstructor))]
public class LevelConstructor : MonoBehaviour
{
    [SerializeField] private int _verticalRange;
    [SerializeField] private int _horizontalRange;
    [SerializeField] private MapElementPool _pool;
    [SerializeField] private GameObject _startTileGameObject;

    private ITile _startTile;
    private List<ITile> _currentTiles;
    private bool _isFinishGenerateTiles = false;
    private FogConstructor _fogConstructor;
    private EnemiesConstructor _enemiesConstructor;

    private void OnValidate()
    {
        _startTileGameObject = _startTileGameObject.GetComponent<ITile>() == null ? null : _startTileGameObject;
        _startTile = _startTileGameObject.GetComponent<ITile>();
    }

    private void OnDestroy()
    {
        _currentTiles.Clear();
    }

    private void Start()
    {
        Time.timeScale = 1;
        _startTile.CheckPosition += GenerateLevel;
        _currentTiles = new List<ITile>();
        _currentTiles.Add(_startTile);
        _pool = FindObjectOfType<MapElementPool>();
        _fogConstructor = GetComponent<FogConstructor>();
        _enemiesConstructor = GetComponent<EnemiesConstructor>();
    }

    public List<ITile> GetZLine(float zPosition) => _currentTiles.Where(t => t.GetPosition().z == zPosition).ToList();

    public List<ITile> GetTiles() => _currentTiles;

    private void GenerateLevel(ITile currentTile)
    {
        float tileXSize = currentTile.GetSize().x;
        float tileZSize = currentTile.GetSize().z;

        List<Vector3> requiredCoordinates = new List<Vector3>();

        for (int i = _horizontalRange * -1; i <= _horizontalRange; i++)
        {
            for (int j = (_verticalRange / 2) * -1; j <= _verticalRange; j++)
            {
                float xPosition = currentTile.GetPosition().x + i * tileXSize;
                float zPosition = currentTile.GetPosition().z + j * tileZSize;
                requiredCoordinates.Add(new Vector3(xPosition, 0, zPosition));
            }
        }

        foreach (var coordinate in requiredCoordinates)
        {
            if (_currentTiles.Where(tile => tile.GetPosition() == coordinate).ToArray().Count() == 0)
                _currentTiles.Add(GenerateTile(coordinate));
        }

        if (!_isFinishGenerateTiles)
        {
            _isFinishGenerateTiles = true;
            _fogConstructor.StartGenerate(this, _startTile);
        }

        _enemiesConstructor.GenerateEnemeSpawners(_currentTiles.ToArray(), currentTile);
    }

    private void ExitLevel()
    {
        foreach(var tile in _currentTiles)
        {
            if (!tile.IsHaveFog)
                _pool.ReturnToPool(tile);
        }
    }

    private ITile GenerateTile(Vector3 position, bool isFirstTile = false)
    {
        ITile tile;

        if (_pool.IsPositionEmpty(position))
        {
            tile = _pool.GetTile();
            tile.ReturningToPool += delegate (ITile currentTile)
            {
                _currentTiles.Remove(currentTile);
            };
            tile.SetPosition(position);
            tile.CheckPosition += GenerateLevel;
        }
        else
        {
            tile = _pool.GetTile(position);
            tile.CheckPosition += GenerateLevel;
            tile.ReturningToPool += delegate (ITile currentTile)
            {
                _currentTiles.Remove(currentTile);
            };
        }
        return tile;
    }

    private void SubscribeOnTileAction(ITile tile)
    {
        tile.ReturningToPool += delegate (ITile currentTile)
        {
            _currentTiles.Remove(currentTile);
        };
        tile.CheckPosition += GenerateLevel;
    }
}
