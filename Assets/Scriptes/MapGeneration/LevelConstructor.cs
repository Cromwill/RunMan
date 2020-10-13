using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelConstructor : MonoBehaviour
{
    [SerializeField] private int _verticalRange;
    [SerializeField] private int _horizontalRange;
    [SerializeField] private GameObject _tilePrefabGameObject;
    [SerializeField] private MapElementPool _pool;
    [SerializeField] private GameObject _startTileGameObject;
    
    [Range(0, 100)]
    [SerializeField] private float _level;

    private MapTile[] _tilesOnScene;
    private Transform _cameraTransform;
    private BarrierGenerator _barrierGenerator;
    private ITile _tilePrefab;
    private ITile _startTile;
    private List<ITile> _currentTiles;

    private void OnValidate()
    {
        _tilePrefabGameObject = _tilePrefabGameObject.GetComponent<ITile>() == null ? null : _tilePrefabGameObject;
        _startTileGameObject = _startTileGameObject.GetComponent<ITile>() == null ? null : _startTileGameObject;
    }

    private void Awake()
    {
        _tilePrefab = _tilePrefabGameObject != null ? _tilePrefabGameObject.GetComponent<ITile>() : null;
        _startTile = _startTileGameObject != null ? _startTileGameObject.GetComponent<ITile>() : null;
        _startTile.CheckPosition += GenerateLevel;
        _currentTiles = new List<ITile>();
        _currentTiles.Add(_startTile);
    }

    private void Start()
    {

    }

    private void Update()
    {

    }

    private void GenerateLevel(ITile currentTile)
    {
        float tileXSize = currentTile.GetSize().x;
        float tileZSize = currentTile.GetSize().z;

        float maxXPosition = currentTile.GetPosition().x + tileXSize * _horizontalRange;
        float minXPosition = currentTile.GetPosition().x - tileXSize * _horizontalRange;
        float maxZPosition = currentTile.GetPosition().z + tileZSize * _verticalRange;
        float minZPosition = currentTile.GetPosition().z - tileZSize * _verticalRange;


        List<Vector3> requiredCoordinates = new List<Vector3>();

        for(int i = _horizontalRange * -1; i <= _horizontalRange; i++)
        {
            for(int j = _verticalRange * -1; j <= _verticalRange; j++)
            {
                float xPosition = currentTile.GetPosition().x + i * tileXSize;
                float zPosition = currentTile.GetPosition().z + j * tileZSize;
                requiredCoordinates.Add(new Vector3(xPosition, 0, zPosition));
            }
        }

        foreach(var coordinate in requiredCoordinates)
        {
            if(_currentTiles.Where(tile => tile.GetPosition() == coordinate).ToArray().Count() == 0)
            {
                var tile = GenerateTile(coordinate);
                tile.CheckPosition += GenerateLevel;
                _currentTiles.Add(tile);
            }
        }
    }

    private ITile GenerateTile(Vector3 position, bool isFirstTile = false)
    {
        ITile tile = Instantiate(_tilePrefabGameObject).GetComponent<ITile>();
        tile.SetPosition(position);
        return tile;
    }
}
