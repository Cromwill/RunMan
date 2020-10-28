using System;
using UnityEngine;


public class Player : MonoBehaviour, IDeadable
{
    [SerializeField] private int _playerId;
    [SerializeField] private int _controlType;
    [SerializeField] private float _speed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private ScoreVieweronlevel _scoreViewer;

    private Rigidbody _selfRigidbody;

    public event Action Deading;

    public ScoreCounter scoreCounter { get; private set; }

    private void Awake()
    {
        _selfRigidbody = GetComponent<Rigidbody>();
        scoreCounter = GetComponent<ScoreCounter>();
        scoreCounter.Initialization(_scoreViewer);
    }

    private void FixedUpdate()
    {
        Move();
        scoreCounter.DistanceColculate();
    }

    public void Turn(RotateDirection direction)
    {
        float angle = (_rotationSpeed * Time.deltaTime) * (int)direction;
        transform.Rotate(new Vector3(0, angle, 0), Space.Self);
    }

    public void Dead()
    {
        Deading?.Invoke();
    }

    private void Move()
    {
        _selfRigidbody.velocity = transform.forward.normalized * _speed * Time.fixedDeltaTime;
    }
}

public enum RotateDirection
{
    Left = -1,
    Right = 1
}