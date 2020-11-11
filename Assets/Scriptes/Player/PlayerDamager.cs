using System;
using UnityEngine;

public class PlayerDamager : MonoBehaviour
{
    [SerializeField] private int _bulletCount;
    [SerializeField] private ArmorInfo _armorViewer;
    [SerializeField] private float _shootSpeed;

    private PlayerDamageZone _damageZone;

    public event Action BulletsRunOut;

    private void Start()
    {
        _damageZone = GetComponentInChildren<PlayerDamageZone>();
        _damageZone.FindedEnemies += Shoot;
        _armorViewer.Show(_bulletCount);
    }

    private void Shoot(Enemy enemy)
    {
        if (_bulletCount > 0)
        {
            _bulletCount--;
            _armorViewer.Show(_bulletCount);
            enemy.Dead();
        }
    }
}
