using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;

public class EnemiesSpawner : MonoBehaviour, ISearchablePlayers
{
    [SerializeField] protected int _maxEnemiesCount;
    [SerializeField] protected Enemy[] _enemies;
    [SerializeField] protected float _spawnTime;

    protected int _enemyCounter;
    protected List<Enemy> _enemiesOnScene;
    protected Player _player;
    

    protected event Action<Player> _playerFounded;
    private void Start()
    {
        _enemiesOnScene = new List<Enemy>();
        StartCoroutine(SpawnEnemy());
    }

    public void OnPlayerFounded(Player player)
    {
        _playerFounded?.Invoke(player);
    }

    protected virtual IEnumerator SpawnEnemy()
    {
        while (_enemyCounter < _maxEnemiesCount)
        {
            float randomPositionX = Random.Range(-1.5f, 1.51f);
            float randomPositionY = Random.Range(-1.5f, 1.51f);

            randomPositionX = Mathf.Sign(randomPositionX) > 0 ? randomPositionX + 1.0f : randomPositionX - 1.0f;
            randomPositionY = Mathf.Sign(randomPositionY) > 0 ? randomPositionY + 1.0f : randomPositionY - 1.0f;

            Vector3 spawnPoint = new Vector3(randomPositionX, 0, randomPositionY);
            Enemy enemy = Instantiate(_enemies[Random.Range(0, _enemies.Length)], transform.position + spawnPoint, Quaternion.Euler(0, Random.Range(0,360), 0));
            _playerFounded += enemy.SetPlayer;
            _enemyCounter++;
            yield return new WaitForSeconds(_spawnTime);
        }
    }
}
