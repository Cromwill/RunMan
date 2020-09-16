using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//класс для ракеты
public class Bullet : MonoBehaviour
{
    public float speed;

    private Transform player;


    void Start()
    {
        player = FindObjectOfType<Player>().transform;
        speed *= Time.fixedDeltaTime;
    }


    void FixedUpdate()
    {
        /*
        Vector3 pDir = Vector3.Normalize(player.position - transform.position);
        pDir.y = 0;
        transform.Translate(pDir.normalized * speed * Time.deltaTime);*/
        transform.LookAt(player.position + Vector3.up);
        transform.position += transform.forward * speed;
    }
}
