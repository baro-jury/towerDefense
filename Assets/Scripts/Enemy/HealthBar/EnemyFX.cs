using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyFX : MonoBehaviour
{
    [SerializeField] private Transform textDamageSpawnPosition;
    private Enemy _enemy;

    void Start()
    {

        _enemy = GetComponent<Enemy>();
    }

    public void EnemyHit(Enemy enemy, float damage)
    {
        print("we hitting");
        if (_enemy == enemy)
        {
            //GameObject newInstance = DamageTextManager.Instance.Pooler.GetInstanceFromPool();
            //TextMeshProUGUI damageText = newInstance.GetComponent<DamageText>().DmgText;
            //damageText.text = damage.ToString();

            //newInstance.transform.SetParent(textDamageSpawnPosition);
            //newInstance.transform.position = textDamageSpawnPosition.position;
            //newInstance.SetActive(true);
        }
    }

    private void OnEnable()
    {
        //Projectile.OnEnemyHit += EnemyHit;
    }

    private void OnDisable()
    {

        //Projectile.OnEnemyHit -= EnemyHit;
    }
}