using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//класс запуска ракет
public class RocketLauncher : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject rocket;
    private Transform player;

    void Start()
    {
        player = FindObjectOfType<Player>().transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.LookAt(player);
    }
    public void Launch()
    {
        Instantiate(rocket, spawnPoint.position, spawnPoint.rotation);
    }
}
