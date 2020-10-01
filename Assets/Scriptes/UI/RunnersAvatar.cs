using UnityEngine;

public class RunnersAvatar : MonoBehaviour, IBuyableObject
{
    [SerializeField] private float _speed;
    [SerializeField] private bool _isPresentation;

    [SerializeField] private float _price;
    [SerializeField] private int _playerId;
    [SerializeField] private string _type;

    public float Price => _price;
    public string Type => _type;
    public int Id => _playerId;

    private void Update()
    {
        if (_isPresentation)
            transform.Rotate(0, 1 * _speed * Time.deltaTime, 0, Space.World);
    }
}
