using UnityEngine;

public class Enemy : MonoBehaviour, IDeadable
{
    [SerializeField] private int _speed;
    [SerializeField] private EffectCicle _deadEffect;
    [SerializeField] private float _maxTurnSpeed;
    [SerializeField] private float _minTurnSpeed;
    [SerializeField] private float _defualtLife;

    private Transform _player;
    private Rigidbody _selfRigidbody;
    private Animator _selfAnimator;

    private Collider _selfColider;
    private float _turnSpeed;

    void Start()
    {
        _player = FindObjectOfType<Player>().transform;
        _selfRigidbody = GetComponent<Rigidbody>();
        _selfColider = GetComponent<CapsuleCollider>();
        _turnSpeed = Random.Range(_minTurnSpeed, _maxTurnSpeed);
    }

    private void FixedUpdate()
    {
        Turn();
        _selfRigidbody.velocity = transform.forward.normalized * _speed * Time.fixedDeltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Player>() != null)
        {
            collision.gameObject.GetComponent<Player>().Dead();
        }
    }

    public void Dead()
    {
        EffectCicle effectCicle = Instantiate(_deadEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);

    }

    public void AddDamage()
    {

    }

    private void Turn()
    {
        Vector3 direction = _player.transform.position - transform.position;
        float step = _turnSpeed * Time.fixedDeltaTime;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, direction.normalized, step, 0.0f);
        Debug.DrawRay(transform.position, newDirection, Color.red, 2.0f);

        transform.rotation = Quaternion.LookRotation(newDirection);
    }

}
