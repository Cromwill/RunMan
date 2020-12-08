using UnityEngine;
using System;
using Random = UnityEngine.Random;
using System.Collections;

public class Enemy : MonoBehaviour, IDeadable
{
    [SerializeField] protected int _speed;
    [SerializeField] protected EffectCicle _deadEffect;
    [SerializeField] protected float _maxTurnSpeed;
    [SerializeField] protected float _minTurnSpeed;
    [SerializeField] protected float _defualtLife;
    [SerializeField] protected float _reactionTime;

    protected Transform _player;
    protected Rigidbody _selfRigidbody;
    protected Animator _selfAnimator;

    protected float _turnSpeed;
    protected bool _isPlayerFounded = false;
    protected event Action<Enemy> Deading;
    protected bool _isDead = false;

    void Start()
    {
        _selfRigidbody = GetComponent<Rigidbody>();
        _selfAnimator = GetComponent<Animator>();
        _selfAnimator.SetBool("Stop", true);
        _turnSpeed = Random.Range(_minTurnSpeed, _maxTurnSpeed);
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Player>() != null && !_isDead)
        {
            collision.gameObject.GetComponent<Player>().Dead();
        }
    }

    public virtual void SetPlayer(Player player)
    {
        _player = player.GetComponent<Transform>();
        transform.LookAt(_player);
    }

    public virtual void Dead()
    {
        Deading?.Invoke(this);
    }

    public void AddDamage(float damage)
    {
        _defualtLife -= damage;
        if (_defualtLife <= 0)
            Dead();
    }
}
