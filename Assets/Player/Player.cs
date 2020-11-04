using System;
using UnityEngine;


public class Player : MonoBehaviour, IDeadable
{
    [SerializeField] private int _playerId;
    [SerializeField] private int _controlType;
    [SerializeField] private float _accelerationTime;
    [SerializeField] private float _minSpeed;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private ScoreVieweronlevel _scoreViewer;

    private Rigidbody _selfRigidbody;
    private float _speed;

    public event Action Deading;

    public ScoreCounter scoreCounter { get; private set; }

    private void Awake()
    {
        _selfRigidbody = GetComponent<Rigidbody>();
        scoreCounter = GetComponent<ScoreCounter>();
        scoreCounter.Initialization(_scoreViewer);
        _speed = (_maxSpeed + _minSpeed) / 2;
    }

    private void FixedUpdate()
    {
        Move();
        scoreCounter.DistanceColculate();
    }

    public void Turn(RotateDirection direction)
    {
        _speed = (_maxSpeed + _minSpeed) / 2;
        float angle = (_rotationSpeed * Time.deltaTime) * (int)direction;
        transform.Rotate(new Vector3(0, angle, 0), Space.Self);
    }

    public void Dead()
    {
        Deading?.Invoke();
    }

    private void Move()
    {
        if(_speed < _maxSpeed)
        {
            _speed += (_maxSpeed - _minSpeed) / _accelerationTime * Time.fixedDeltaTime;
            if (_speed >= _maxSpeed) _speed = _maxSpeed;
        }

        _selfRigidbody.velocity = transform.forward.normalized * _speed * Time.fixedDeltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<MapElement>() != null)
            _speed = _minSpeed;
    }
}

public enum RotateDirection
{
    Left = -1,
    Right = 1
}