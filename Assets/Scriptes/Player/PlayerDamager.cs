using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamager : MonoBehaviour
{
    private PlayerDamageZone _damageZone;
    

    private void Start()
    {
        _damageZone = GetComponentInChildren<PlayerDamageZone>();
        _damageZone.FindedEnemies += Shoot;
    }

    private void Shoot(Enemy enemy)
    {
        Debug.Log("shoot");
        enemy.Dead();
    }
}
