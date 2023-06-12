using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayController : MonoBehaviour
{
    public static GameplayController instance;

    public List<Button> btnsSetupTurret;
    //public List<GameObject> turretContainers;
    public List<GameObject> turrets;

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
        for (int i = 0; i < btnsSetupTurret.Count; i++)
        {
            var obj = new GameObject("TurretContainer" + (i + 1));
            obj.transform.position = new Vector3
                (btnsSetupTurret[i].transform.position.x, btnsSetupTurret[i].transform.position.y);
            btnsSetupTurret[i].onClick.AddListener(delegate { _SetupTurret(obj.transform); });
        }
    }

    public void _SetupTurret(Transform turretContainer)
    {
        if (turretContainer.childCount == 0)
        {
            _BuildTurret(turretContainer);
        }
        else
        {
            _EditTurret(turretContainer);
        }
    }

    public void _BuildTurret(Transform turretContainer)
    {
        Debug.Log("build");
        Instantiate(turrets[0], turretContainer);
    }

    public void _EditTurret(Transform turretContainer)
    {
        Debug.Log("edit");
    }

    private void OnEnable()
    {
        //Enemy.OnEndReached += EnemyHit;
    }

    private void OnDisable()
    {
        //Enemy.OnEndReached -= EnemyHit;
    }
}
