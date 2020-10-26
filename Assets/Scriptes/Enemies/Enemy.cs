using UnityEngine;

//общий класс монстров для огра и зомби
public class Enemy : MonoBehaviour, IDeadable
{
    [SerializeField] private int _speed;
    [SerializeField] private EffectCicle _deadEffect;

    private Transform player;
    private Rigidbody _selfRigidbody;
    private Animator _selfAnimator;

    private Collider _selfColider;

    void Start()
    {
        player = FindObjectOfType<Player>().transform;
        _selfRigidbody = GetComponent<Rigidbody>();
        _selfColider = GetComponent<CapsuleCollider>();
    }


    void FixedUpdate()
    {
        Vector3 pDir = Vector3.Normalize(player.position - transform.position);
        transform.LookAt(player.transform);
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
        Debug.Log("Enemy dead");
        EffectCicle effectCicle = Instantiate(_deadEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);

    }
}
