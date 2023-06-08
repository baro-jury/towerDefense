using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretUpgrade : MonoBehaviour
{
    [SerializeField] private int upgradeInitialCost;
    [SerializeField] private int upgradeCostIncremental;
    [SerializeField] private int damageIncremental;
    [SerializeField] private float delayReduce;
    [Header("Sell")]
    [Range(0, 1)]
    [SerializeField] private float sellPert;

    public float SellPerc { get; set; }
    public int UpgradeCost { get; set; }
    public int Level { get; set; }

    private TurretProjectile _turretProjectile;

    void Start()
    {
        _turretProjectile = GetComponent<TurretProjectile>();
        UpgradeCost = upgradeInitialCost;
        SellPerc = sellPert;
        Level = 1;
    }

    public void UpgradeTurret()
    {
        if (CurrencySystem.instance.TotalCoins >= UpgradeCost)
        {
            _turretProjectile.Damage += damageIncremental;
            _turretProjectile.DelayPerShot -= delayReduce;
            UpdateUpgrade();
        }
    }

    public int GetSellValue()
    {
        int sellValue = Mathf.RoundToInt(UpgradeCost * SellPerc);
        return sellValue;
    }

    private void UpdateUpgrade()
    {
        CurrencySystem.instance.RemoveCoins(UpgradeCost);
        UpgradeCost += upgradeCostIncremental;
        Level++;
    }
}
