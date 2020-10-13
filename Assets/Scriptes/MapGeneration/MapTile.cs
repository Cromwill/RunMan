using UnityEngine;
using UnityEngine.UIElements;

public class MapTile : MonoBehaviour
{
    [SerializeField] private float _leftIndent;
    [SerializeField] private float _rightIndent;
    [SerializeField] private float _verticalStep;
    [SerializeField] private float _horizontalStep;

    private MapElementPool _elementsPool;
    private Mesh _mesh;

    private void OnEnable()
    {
        _mesh = GetComponent<MeshFilter>().mesh;
    }

    private void Start()
    {
        _mesh = GetComponent<MeshFilter>().mesh;
    }

    public void GenerateWall(MapElementPool mapElementPool = null)
    {
        if (_elementsPool == null && mapElementPool != null)
            _elementsPool = mapElementPool;


        Bounds bounds = _mesh.bounds;
        int verticalCount = (int)(bounds.size.z / _verticalStep) + 1;

        for(int i = 0; i < 2; i++)
        {
            float indent = i % 2 != 0 ? _leftIndent : _rightIndent;
            float startPosition = bounds.min.z + transform.position.z;

            for(int j = 0; j < verticalCount; j++)
            {
                IMapElement mapElement = _elementsPool.GetNonDestroyObject(transform);
                Vector3 position = new Vector3(indent, 0, startPosition + (j * _verticalStep));
                mapElement.SetElement(position);

                if (Random.Range(0, 4) != 0)
                {
                    mapElement = _elementsPool.GetNonDestroyObject(transform);
                    int sign = indent < 0 ? 1 : -1;
                    float xPosition = Random.Range(indent + sign, indent + _horizontalStep * sign);

                    position = new Vector3(xPosition, 0, startPosition + (j * _verticalStep));
                    mapElement.SetElement(position);
                }
            }
        }
    }

    public float GetHeight() => _mesh.bounds.size.x;

    public void Destroy() => Destroy(gameObject);

    public Vector3 GetPosition() => transform.position;

    public void SetPosition(Vector3 position, BarrierGenerator barriers, bool isFirstTile)
    {
        transform.position = position;

        if (!isFirstTile)
        {
            Bounds bounds = _mesh.bounds;
            int minLinePositionZ = (int)(position.z + bounds.min.z);
            int maxLinePositionZ = (int)(position.z + bounds.max.z);

            for (int i = minLinePositionZ; i < maxLinePositionZ + 1; i++)
            {
                barriers.GenerateBarriers(i);
            }
        }
    }
}