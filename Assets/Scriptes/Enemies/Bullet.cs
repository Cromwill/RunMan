using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//класс для ракеты
public class Bullet : Enemy
{
    void Start()
    {
        _selfRigidbody = GetComponent<Rigidbody>();
        _turnSpeed = _maxTurnSpeed;
    }

    void FixedUpdate()
    {
        if(_isPlayerFounded)
        {
            Turn(_player.position);
            _selfRigidbody.velocity = transform.forward.normalized * _speed * Time.fixedDeltaTime;
        }
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Player>() != null && !_isDead)
        {
            collision.gameObject.GetComponent<Player>().Dead();
        }
        Dead();
    }

    public override void SetPlayer(Player player)
    {
        _player = player.transform;
        _isPlayerFounded = true;
    }

    public override void Dead()
    {
        if(_deadEffect != null)
            Instantiate(_deadEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
        base.Dead();
        
    }

    private void Turn(Vector3 target)
    {
        target = new Vector3(target.x, target.y + 1.0f, target.z);
        Vector3 direction = target - transform.position;
        float step = _turnSpeed * Time.fixedDeltaTime;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, direction.normalized, step, 0.0f);
        Debug.DrawRay(transform.position, newDirection, Color.red, 2.0f);

        transform.rotation = Quaternion.LookRotation(newDirection);
    }

}
