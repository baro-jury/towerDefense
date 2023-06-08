using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [SerializeField] private List<GameObject> prefabs;
    [SerializeField] private int prefabIndex;

    //[SerializeField] private GameObject prefab; 
    [SerializeField] private int poolSize = 10;
    [SerializeField] private GameObject _poolContainer;

    public int NumberOfEnemyType { get { return prefabs.Count; } }
    public int PoolSize { get { return poolSize; } }

    protected List<GameObject> _pool;

    protected virtual void Awake()
    {
        _pool = new List<GameObject>();
        //_poolContainer = new GameObject($"Pool - {prefab.name}");
        AddPrefabsToPool();
    }

    private void AddPrefabsToPool()
    {
        for (int j = 0; j < NumberOfEnemyType; j++)
        {
            prefabIndex = j;
            for (int i = 0; i < PoolSize; i++)
            {
                _pool.Add(CreatePrefab());
            }
        }
    }

    protected virtual GameObject CreatePrefab()
    {
        GameObject obj = Instantiate(prefabs[prefabIndex], _poolContainer.transform);
        obj.name = prefabs[prefabIndex].name + " - " + obj.transform.GetSiblingIndex();
        obj.SetActive(false);
        return obj;
    }

    public GameObject GetNextPrefabFromPool()
    {
        for (int i = 0; i < _pool.Count; i++)
        {
            if (_pool[i].transform.position == _poolContainer.transform.position
                && !_pool[i].activeInHierarchy)
            {
                return _pool[i];
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
