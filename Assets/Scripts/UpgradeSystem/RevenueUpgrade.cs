using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class RevenueUpgrade : UpgradeModule
{
    [Header("Revenue")]
    [SerializeField] private TextMeshProUGUI _revenue;
    
    [Header("Progress")]
    [SerializeField] private TextMeshProUGUI _currentLevel;
    [SerializeField] private TextMeshProUGUI _nextLevel;
    [SerializeField] private TextMeshProUGUI _nextLevelBonus;
    [SerializeField] private Image _progressBar;
    
    private int[] _levels = new[] { 25, 50, 100, 150, 200, 300, 400, 500 };
    private int[] _boosts = new[] { 1, 1, 1, 2, 2, 2, 2, 2 };
    
    public override void Init(BuildingInfoData buildingData, BuildingConfig buildingConfig, DataAccessService dataAccessService)
    {
        base.Init(buildingData, buildingConfig, dataAccessService);
        buildingData.OnRevenueLevelChanged += UpdateProgress;
        UpdateProgress(buildingData.revenueLevel);
    }

    protected override void UpdateProgress(int level)
    {
        int prevLevel = GetPreviousLevel(level);
        int nextLevel = GetNextLevel(level);
    
        _currentLevel.text = level.ToString();
        _nextLevel.text = buildingData.revenueLevel == _levels[^1] ? "Max" : nextLevel.ToString();
        _revenue.text = buildingConfig.CalculateRevenue(level).ToString();
        
        _progressBar.fillAmount = (float)(level - prevLevel) / (nextLevel - prevLevel);

        return;

        int GetPreviousLevel(int currentLevel)
        {
            for (var i = _levels.Length - 1; i >= 0; i--)
            {
                if (currentLevel >= _levels[i])
                    return _levels[i];
            }
            return 0;
        }

        int GetNextLevel(int currentLevel)
        {
            for (var i = 0; i < _levels.Length; i++)
            {
                if (currentLevel < _levels[i])
                {
                    _nextLevelBonus.text = "x" + _boosts[i];
                    return _levels[i];
                }
            }
            return _levels[^1];
        }
    }


    protected override void UpdateUpgradeButton(int value)
    {
        _upgradeCostValue = buildingConfig.Upgrades.CalculateUpgradeRevenueCost(buildingData.revenueLevel);
        base.UpdateUpgradeButton(value);
    }
    
    protected override void IncreaseLevel()
    {
        int upgradeCost = buildingConfig.Upgrades.CalculateUpgradeRevenueCost(buildingData.revenueLevel);

        if (_currencyData.TryDecreaseCoinsCount(upgradeCost))
        {
            buildingData.IncreaseRevenueLevel();
            UpdateUpgradeButton(_currencyData.count);
        }
    }

    public override void OnClose()
    {
        base.OnClose();
        buildingData.OnRevenueLevelChanged -= UpdateProgress;
    }
}