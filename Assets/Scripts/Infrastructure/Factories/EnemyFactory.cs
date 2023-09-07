using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemyFactory : IEnemyFactory
{
    private IStrufeFactory _strufeFactory;
    private List<Enemy> _enemies;
    private GameObject _enemyPrefab;
    private int _pullSize;
    private int _lastIndex;
    private float _enemyToSpawnCoords;
    private float _strufeOffset;
    private const float offset = 1f;
    public EnemyFactory(DiContainer container, GameObject enemyPrefab, int pullSize, IStrufeFactory strufeFactory, Vector3 strufeCollisionPosition)
    {
        _enemies = new List<Enemy>(pullSize);
        _enemyPrefab = enemyPrefab;
        _pullSize = pullSize;
        _strufeFactory = strufeFactory;
        _strufeOffset = strufeCollisionPosition.z;
        _enemyToSpawnCoords = (strufeFactory.StrufeWidth - offset * 2) / 2;
        CreateEnemyPull(pullSize, container);
    }

    public void CreateEnemy()
    {
        var enemyToActivate = _enemies[_lastIndex].gameObject;
        enemyToActivate.transform.position = new Vector3(Random.Range(-1 * _enemyToSpawnCoords, _enemyToSpawnCoords), _strufeFactory.Strufes.Count + _strufeOffset, _strufeFactory.Strufes.Count + _strufeOffset);
        enemyToActivate.SetActive(true);

        _lastIndex++;
        _lastIndex %= _enemies.Count;
    }

    public void RemoveAllEnemies()
    {
        _lastIndex = 0;
        foreach (Enemy enemy in _enemies)
            RemoveEnemy(enemy);
    }

    public void RemoveEnemy(Enemy enemy)
    {
        enemy.gameObject.SetActive(false);
    }

    private void CreateEnemyPull(int size, DiContainer container)
    {
        for(int i = 0; i < size; i++)
        {
            var randomRotation = Quaternion.Euler(Random.Range(0, 360f), 0, 0);
            _enemies.Add(container.InstantiatePrefabForComponent<Enemy>(_enemyPrefab,Vector3.zero,randomRotation,null));
        }
    }
}
