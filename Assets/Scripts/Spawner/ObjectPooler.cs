using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [SerializeField] private List<GameObject> prefabs;

    //[SerializeField] private GameObject prefab; 
    //[SerializeField] private int poolSize = 10;
    [SerializeField] private GameObject _poolContainer;

    private List<int> _enemiesInWave;
    private int _poolSize;
    private int _prefabIndex;

    public int PoolSize { get { return _poolSize; } }

    public List<GameObject> pool;

    void Awake()
    {
        SetupPooler();
    }

    protected virtual void Start()
    {
        //SetupPooler();
    }

    public void SetupPooler()
    {
        pool = new List<GameObject>();
        _enemiesInWave = LevelManager.levelData.Enemies;
        _poolSize = LevelManager.levelData.Enemies.Count;
        //_enemiesInWave = LevelManager.levelData.Waves[0].EnemyIndexes;
        //_poolSize = LevelManager.levelData.Waves[0].EnemyIndexes.Count;
        AddPrefabsToPool();
    }

    private void AddPrefabsToPool()
    {
        for (int i = 0; i < PoolSize; i++)
        {
            _prefabIndex = _enemiesInWave[i];
            pool.Add(CreatePrefab());
        }
    }

    protected virtual GameObject CreatePrefab()
    {
        GameObject obj = Instantiate(prefabs[_prefabIndex], _poolContainer.transform);
        obj.SetActive(false);
        return obj;
    }

    public GameObject GetNextPrefabFromPool()
    {
        for (int i = 0; i < pool.Count; i++)
        {
            if (pool[i].transform.position == _poolContainer.transform.position
                && !pool[i].activeInHierarchy)
            {
                return pool[i];
            }
        }
        return CreatePrefab();
    }

    public static void ReturnToPool(GameObject instance)
    {
        instance.SetActive(false);
    }

    public static IEnumerator ReturnToPoolWithDelay(GameObject instance, float delay)
    {
        yield return new WaitForSeconds(delay);
        instance.SetActive(false);
    }
}
