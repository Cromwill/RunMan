using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGeneration : MonoBehaviour, ITile
{
    public Vector3[] _notDestroyObjectPositions;
    public Vector3[] _destroyPositions;

    private Mesh _mesh;
    private MapElementPool _pool;

    public bool IsInThePool { get; set; } = true;
    public bool IsHaveFog { get; private set; }
    public event Action<ITile> CheckPosition;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.grey;

        if (_notDestroyObjectPositions != null)
        {
            for (int i = 0; i < _notDestroyObjectPositions.Length; i++)
                Gizmos.DrawCube(transform.position + _notDestroyObjectPositions[i], new Vector3(1, 1, 0.5f));
        }

        if (_destroyPositions != null)
        {
            Gizmos.color = Color.red;

            for (int i = 0; i < _destroyPositions.Length; i++)
                Gizmos.DrawSphere(transform.position + _destroyPositions[i], 0.35f);
        }
    }

    private void Start()
    {
        _mesh = GetComponent<MeshFilter>().mesh;
        _pool = FindObjectOfType<MapElementPool>();
        GenerateTile(_notDestroyObjectPositions, MapElementTypes.NotDestroy);
        GenerateTile(_destroyPositions, MapElementTypes.Destroy);
    }

    public void SetPosition(Vector3 position) => transform.position = position;

    public Vector3 GetPosition() => transform.position;

    public Vector3 GetSize() => _mesh.bounds.size;

    public void AddFog(Fog fog)
    {
        IsHaveFog = true;
        fog.Destriction += ReturnToPool;
    }

    private void ReturnToPool(Fog fog)
    {
        fog.Destriction -= ReturnToPool;
        IsHaveFog = false;
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
