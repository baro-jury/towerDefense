using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static Spawner instance;
    public static Action OnSpawnEnemy;

    //[Header("Settings")]
    //[SerializeField] private int enemyCount = 10;

    [Header("Fixed Delay")]
    [SerializeField] private float spawnTimeInterval;

    public float SpawnTimer { get; set; }
    public int EnemyCount { get; set; }
    public int EnemiesSpawned { get; set; }

    private ObjectPooler _pooler;

    void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Awake()
    {
        MakeInstance();
    }

    void Start()
    {
        SpawnTimer = spawnTimeInterval;
        EnemiesSpawned = 0;
        _pooler = GetComponent<ObjectPooler>();
        //EnemyCount = _pooler.PoolSize;
        EnemyCount = LevelManager.levelData.Enemies.Count;
        //enemyCount = LevelManager.levelData.Waves[0].EnemyIndexes.Count;
    }

    void Update()
    {
        SpawnTimer -= Time.deltaTime;
        if (SpawnTimer < 0)
        {
            SpawnTimer = spawnTimeInterval;
            if (EnemiesSpawned < EnemyCount)
            {
                EnemiesSpawned++;
                SpawnEnemy();
            }
        }
    }
    private void SpawnEnemy()
    {
        GameObject enemy = _pooler.GetNextPrefabFromPool();
        enemy.SetActive(true);
        OnSpawnEnemy?.Invoke();
    }

    public bool NoEnemies()
    {
        for (int i = 0; i < _pooler.pool.Count; i++)
        {
            if (_pooler.pool[i].activeInHierarchy)
            {
                return false;
            }
        }
        return true;
    }
}
