using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] private Transform magazine;
    [SerializeField] private GameObject prefab;
    //[SerializeField] private List<GameObject> prefabs;
    //[SerializeField] private int prefabIndex;

    private GameObject _projectile;

    void Awake()
    {
        AddProjectilesToMagazine();
    }

    protected void AddProjectilesToMagazine()
    {
        GameObject obj = Instantiate(prefab, magazine);
        //GameObject obj = Instantiate(prefabs[prefabIndex], magazine);
        obj.SetActive(false);
        _projectile = obj;
    }

    public GameObject GetProjectileFromMagazine()
    {
        if (_projectile == null)
        {
            AddProjectilesToMagazine();
        }
        return _projectile;

        //if (_projectile == null ||
        //    (_projectile.activeInHierarchy && magazine.childCount < 2))
        //{
        //    AddProjectilesToMagazine();
        //}
        //return _projectile;
    }

}
