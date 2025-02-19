using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class ProductionTimeUpgrade : UpgradeModule
{
    private const int _maxUpdateLevel = 100;
    
    [Header("Progress")]
    [SerializeField] private TextMeshProUGUI _currentLevel;
    [SerializeField] private TextMeshProUGUI _timeInSeconds;
    [SerializeField] private TextMeshProUGUI _upgradeInPercent;
    [SerializeField] private Image _progressBar;

    public override void Init(BuildingInfoData buildingData, BuildingConfig buildingConfig, DataAccessService dataAccessService)
    {
        base.Init(buildingData, buildingConfig, dataAccessService);
        buildingData.OnProduceSpeedLevelChanged += UpdateProgress;
        UpdateProgress(buildingData.produceSpeedLevel);
    }
    
    protected override void UpdateUpgradeButton(int value)
    {
        _upgradeCostValue = buildingConfig.Upgrades.CalculateUpgradeSpeedCost(buildingData.produceSpeedLevel);
        base.UpdateUpgradeButton(value);
    }
    
    protected override void IncreaseLevel()
    {
        if (_currencyData.TryDecreaseCoinsCount(_upgradeCostValue))
        {
            buildingData.IncreaseProduceSpeedLevel();
            UpdateUpgradeButton(_currencyData.count);
        }
    }

    protected override void UpdateProgress(int level)
    {
        _currentLevel.text = level.ToString();
        _progressBar.fillAmount = (float)level / _maxUpdateLevel;
        _timeInSeconds.text = buildingConfig.CalculateSpeed(level).ToString("F2") + "s";
        _upgradeInPercent.text =
            (int)(buildingData.produceSpeedLevel * buildingConfig.SpeedUpgradeMultiplierInPercent) + "%";
    }
}