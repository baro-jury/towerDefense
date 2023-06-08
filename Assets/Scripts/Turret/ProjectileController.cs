using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] private List<GameObject> prefabs;
    [SerializeField] private int prefabIndex;
    [SerializeField] private Transform _poolContainer;

    private GameObject _projectile;

    void Awake()
    {
        AddProjectilesToPool();
    }

    protected void AddProjectilesToPool()
    {
        GameObject obj = Instantiate(prefabs[prefabIndex], _poolContainer);
        obj.name = prefabs[prefabIndex].name + " - " + obj.transform.GetSiblingIndex();
        obj.SetActive(false);
        _projectile = obj;
    }

    public GameObject GetProjectileFromPool()
    {
        //if (!_projectile.activeInHierarchy)
        //{
        //    return _projectile;
        //}
        //AddProjectilesToPool(_projectile);
        //return _projectile;

        if (_projectile == null || _projectile.activeInHierarchy)
        {
            AddProjectilesToPool();
        }
        return _projectile;
    }

}
