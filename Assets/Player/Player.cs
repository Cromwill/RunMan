using System;
using System.Collections;
using UnityEngine;


public class Player : MonoBehaviour, IDeadable
{
    [SerializeField] private int _playerId;
    [SerializeField] private int _controlType;
    [SerializeField] private float _accelerationTime;
    [SerializeField] private float _minSpeed;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _currentRotationSpeed;
    [SerializeField] private float _maxRotationSpeed;
    [SerializeField] private ScoreVieweronlevel _scoreViewer;
    [SerializeField] private float _jumpForce;

    private Rigidbody _selfRigidbody;
    private float _speed;
    private float _rotationSpeed;

    public event Action Deading;

    public ScoreCounter scoreCounter { get; private set; }

    private void Awake()
    {
        _selfRigidbody = GetComponent<Rigidbody>();
        scoreCounter = GetComponent<ScoreCounter>();
        scoreCounter.Initialization(_scoreViewer);
        _speed = (_maxSpeed + _minSpeed) / 2;
        _rotationSpeed = _currentRotationSpeed;
    }

    private void FixedUpdate()
    {
        Move();
        scoreCounter.DistanceColculate();
        if(Input.GetKeyDown(KeyCode.Space))
        {
            _selfRigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }
    }

    public void Turn(RotateDirection direction)
    {
        //_speed = (_maxSpeed + _minSpeed) / 2;
        float angle = (_rotationSpeed * Time.deltaTime) * (int)direction;
        transform.Rotate(new Vector3(0, angle, 0), Space.Self);
    }

    public void Dead()
    {
        Deading?.Invoke();
    }

    private void Move()
    {
        if (_speed < _maxSpeed)
        {
            _speed += (_maxSpeed - _minSpeed) / _accelerationTime * Time.fixedDeltaTime;
            if (_speed >= _maxSpeed) _speed = _maxSpeed;
        }

        _selfRigidbody.velocity = transform.forward.normalized * _speed * Time.fixedDeltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<MapElement>() != null)
        {
            _speed = _minSpeed;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.GetComponent<MapElement>() != null)
        {
            _rotationSpeed = _maxRotationSpeed;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.GetComponent<MapElement>() != null)
        {
            _rotationSpeed = _currentRotationSpeed;
        }
    }

    private IEnumerator ReturnTurnSpeed()
    {
        yield return null;
    }
}

public enum RotateDirection
{
    Left = -1,
    Right = 1
}