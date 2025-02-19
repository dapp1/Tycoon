using System;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(BuildingConfig), menuName = "Configs/" + nameof(BuildingConfig))]
public class BuildingConfig : ScriptableObject
{
    [Header("Default settings")]
    [SerializeField] private string _id;
    [SerializeField] private float _defaultRevenue;
    [SerializeField] private float _defaultProductionSpeed;
    
    [Header("Multipliers")]
    [SerializeField] private float _revenueMultiplier;
    [SerializeField] private float _productionSpeedMultiplier;
    
    [Header("Upgrade")]
    [SerializeField] private UpgradeValues _upgradeValues;

    private int[] _boosts = new[] { 1, 1, 1, 2, 2, 2, 2, 2 };
    
    public string BuildingID => _id;
    public UpgradeValues Upgrades => _upgradeValues;
    public float SpeedUpgradeMultiplierInPercent => (_productionSpeedMultiplier - 1) * 100;
    
    public int CalculateRevenue(int revenueLevel)
    {
        return (int)((_defaultRevenue * Mathf.Pow(_revenueMultiplier, revenueLevel)));
    }

    public float CalculateSpeed(int prodSpeedLevel)
    {
        return _defaultProductionSpeed / Mathf.Pow(_productionSpeedMultiplier, prodSpeedLevel);
    }

    [Serializable]
    public struct UpgradeValues
    {
        [Header("Upgrade Cost")]
        [SerializeField] private float _revenueUpgradeCost;
        [SerializeField] private float _workersUpgradeCost;
        [SerializeField] private float _speedUpgradeCost;
    
        [Header("Upgrade Multiplier")]
        [SerializeField] private float _revenueUpgradeMultiplier;
        [SerializeField] private float _levelUpgradeMultiplier;
        [SerializeField] private float _speedUpgradeMultiplier;
        
        
        public int CalculateUpgradeRevenueCost(int currentRevenueLevel)
        {
            return (int)(_revenueUpgradeCost * Mathf.Pow(_revenueUpgradeMultiplier, currentRevenueLevel));
        }
        
        public int CalculateUpgradeWorkersCost(int currentWorkersCount)
        {
            return (int)(_workersUpgradeCost * Mathf.Pow(_levelUpgradeMultiplier, currentWorkersCount));
        }
        
        public int CalculateUpgradeSpeedCost(int currentSpeedLevel)
        {
            return (int)(_speedUpgradeCost * Mathf.Pow(_speedUpgradeMultiplier, currentSpeedLevel));
        }
    }
}

