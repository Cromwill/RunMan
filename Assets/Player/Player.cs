using UnityEngine;

//класс отвечающий за действия персонажа во время игрвоого процесса
public class Player : MonoBehaviour
{
    [SerializeField] private int _playerId;
    [SerializeField] private int _controlType = 1;
    [SerializeField] private float _speed = 5F;
    [SerializeField] private float _rotationSpeed = 200F;

    private MobileInputter _joystick;
    private float _distanceCovered = 0;

    private Rigidbody _selfRigidbody;


    private void Awake()
    {
        _selfRigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void OnTriggerEnter(Collider col)
    {

    }

    private void Move()
    {
        _selfRigidbody.velocity = transform.forward.normalized * _speed * Time.fixedDeltaTime;
    }

    public void Turn(RotateDirection direction)
    {
        float angle = (_rotationSpeed * Time.deltaTime) * (int)direction;
        transform.Rotate(new Vector3(0, angle, 0), Space.Self);
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