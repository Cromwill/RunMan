using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageZone : MonoBehaviour
{
    public event Action<Enemy> FindedEnemies;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (other.GetComponent<Enemy>() != null)
        {
            Debug.Log("Finded enemies");
            FindedEnemies?.Invoke(other.GetComponent<Enemy>());
        }
    }
}
