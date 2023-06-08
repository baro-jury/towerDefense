using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static Spawner instance;

    [Header("Settings")]
    [SerializeField] private int enemyCount = 10;

    [Header("Fixed Delay")]
    [SerializeField] private float spawnTimeInterval;

    public float SpawnTimer { get; set; }

    private int _enemiesSpawned;

    private ObjectPooler _pooler;

    void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Awake()
    {
        MakeInstance();
    }

    void Start()
    {
        SpawnTimer = spawnTimeInterval;
        _pooler = GetComponent<ObjectPooler>();
        enemyCount = _pooler.NumberOfEnemyType * _pooler.PoolSize;
    }

    void Update()
    {
        SpawnTimer -= Time.deltaTime;
        if (SpawnTimer < 0)
        {
            SpawnTimer = spawnTimeInterval;
            if (_enemiesSpawned < enemyCount)
            {
                _enemiesSpawned++;
                SpawnEnemy();
            }
        }
    }
    private void SpawnEnemy()
    {
        GameObject enemy = _pooler.GetNextPrefabFromPool();
        enemy.SetActive(true);
    }
}
