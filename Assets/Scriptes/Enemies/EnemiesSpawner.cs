using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class EnemiesSpawner : MonoBehaviour
{
    [SerializeField] private int _maxEnemiesCount;
    [SerializeField] private Enemy[] _enemies;
    [SerializeField] private float _spawnTime;

    private int _enemyCounter;
    private List<Enemy> _enemiesOnScene;
    private Player _player;
    private void Start()
    {
        _enemiesOnScene = new List<Enemy>();
        _player = FindObjectOfType<Player>();
        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        while (_enemyCounter < _maxEnemiesCount)
        {
            float randomPosition = Random.Range(-1.5f, 1.51f);
            Vector3 spawnPoint = new Vector3(randomPosition, 0, randomPosition);
            Enemy enemy = Instantiate(_enemies[Random.Range(0, _enemies.Length)], transform.position + spawnPoint, Quaternion.identity);
            enemy.transform.LookAt(_player.transform);
            _enemyCounter++;
            yield return new WaitForSeconds(_spawnTime);
        }
    }
}
