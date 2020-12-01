using System;
using UnityEngine;

public class TileGeneration : MonoBehaviour, ITile
{
    public Vector3[] NotDestroyObjectPositions;
    public Vector3[] DestroyPositions;
    public Vector3 EnemiesSpawnDot;

    private Mesh _mesh;
    private MapElementPool _pool;
    private EnemiesSpawner _spawner;

    public bool IsInThePool { get; set; } = true;
    public bool IsHaveFog { get; private set; }
    public event Action<ITile> CheckPosition;
    public event Action<ITile> ReturningToPool;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.grey;

        if (NotDestroyObjectPositions != null)
        {
            for (int i = 0; i < NotDestroyObjectPositions.Length; i++)
                Gizmos.DrawCube(transform.position + NotDestroyObjectPositions[i], new Vector3(1, 1, 0.5f));
        }

        if (DestroyPositions != null)
        {
            Gizmos.color = Color.red;

            for (int i = 0; i < DestroyPositions.Length; i++)
                Gizmos.DrawSphere(transform.position + DestroyPositions[i], 0.35f);
        }

        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position + EnemiesSpawnDot, 0.35f);
    }

    private void Start()
    {
        _mesh = GetComponent<MeshFilter>().mesh;
        _pool = FindObjectOfType<MapElementPool>();
        GenerateTile(NotDestroyObjectPositions, MapElementTypes.NotDestroy);
        GenerateTile(DestroyPositions, MapElementTypes.Destroy);
    }

    public void SetPosition(Vector3 position) => transform.position = position;

    public Vector3 GetPosition() => transform.position;

    public Vector3 GetSize() => _mesh.bounds.size;

    public void AddFog(Fog fog)
    {
        IsHaveFog = true;
        fog.Destriction += ReturnToPool;
    }

    public void AddSpawner(EnemiesSpawner spawner)
    {
        _spawner = spawner;
    }

    private void ReturnToPool(Fog fog)
    {
        fog.Destriction -= ReturnToPool;
        IsHaveFog = false;
        ReturningToPool?.Invoke(this);

        if (_spawner != null)
            Destroy(_spawner.gameObject);
        _pool.ReturnToPool(this);
    }

    private void GenerateTile(Vector3[] positions, MapElementTypes mapElementTypes)
    {
        for (int i = 0; i < positions.Length; i++)
        {
            IMapElement prefab = mapElementTypes == MapElementTypes.Destroy ? _pool.GetDestroyObject() : _pool.GetNonDestroyObject();
            IMapElement mapElement = Instantiate((MapElement)prefab, transform);
            mapElement.SetElement(GetPosition() + positions[i]);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Player>() != null)
            CheckPosition?.Invoke(this);
    }
}

public enum MapElementTypes
{
    Destroy,
    NotDestroy
}
