using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

public class EnemyHealth : MonoBehaviour
{
    public static Action<Enemy> OnEnemyKilled;
    public static Action<Enemy> OnEnemyHit;

    [SerializeField] private GameObject healthBarPrefab;
    [SerializeField] private Transform barPosition;
    [SerializeField] private float initialHealth = 10f;
    [SerializeField] private float maxHealth = 10f;

    public float CurrentHealth { get; set; }

    private Image _healthBar;
    private Enemy _enemy;

    void Start()
    {
        CreateHealthBar();
        CurrentHealth = initialHealth;
        _enemy = GetComponent<Enemy>();
    }

    private void CreateHealthBar()
    {
        GameObject newBar = Instantiate(healthBarPrefab, barPosition.position, Quaternion.identity, transform);
        EnemyHealthContainer container = newBar.GetComponent<EnemyHealthContainer>();
        _healthBar = container.FillAmountImage;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            DealDamage(5f);
        }
        _healthBar.fillAmount = Mathf.Lerp(_healthBar.fillAmount, CurrentHealth / maxHealth, Time.deltaTime * 10f);
    }


    public void DealDamage(float damageReceived)
    {
        CurrentHealth -= damageReceived;
        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            Die();
        }
        else
        {
            OnEnemyHit?.Invoke(_enemy);
        }
    }

    private void Die()
    {
        throw new NotImplementedException();
    }

    public void ResetHealth()
    {
        throw new NotImplementedException();
    }
}
