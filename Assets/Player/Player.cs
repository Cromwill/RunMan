using System;
using UnityEngine;

//класс отвечающий за действия персонажа во время игрвоого процесса
public class Player : MonoBehaviour, IDeadable
{
    [SerializeField] private int _playerId;
    [SerializeField] private int _controlType;
    [SerializeField] private float _speed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private ScoreVieweronlevel _scoreViewer;

    private ScoreCounter _scoreCounter;
    private MobileInputter _joystick;
    private Rigidbody _selfRigidbody;

    public event Action Deading;

    public ScoreCounter scoreCounter => _scoreCounter;

    private void Awake()
    {
        _selfRigidbody = GetComponent<Rigidbody>();
        _scoreCounter = GetComponent<ScoreCounter>();
        _scoreCounter.Initialization(_scoreViewer);
    }

    private void FixedUpdate()
    {
        Move();
        _scoreCounter.DistanceColculate();
    }

    public void Turn(RotateDirection direction)
    {
        float angle = (_rotationSpeed * Time.deltaTime) * (int)direction;
        transform.Rotate(new Vector3(0, angle, 0), Space.Self);
    }

    public void Dead()
    {
        Debug.Log("PlayerDead");
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

[System.Serializable]
public class PlayerData
{
    int highScore;
    int bestDistance;

    public PlayerData(int hs, int bd)
    {
        highScore = hs;
        bestDistance = bd;
    }
}