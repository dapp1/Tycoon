using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class WorkersInBuildingUpgrade : UpgradeModule
{
    [Header("Progress")] 
    [SerializeField] private TextMeshProUGUI _workersCount;
    [SerializeField] private Image _maxCount;

    public override void Init(BuildingInfoData buildingData, BuildingConfig buildingConfig, DataAccessService dataAccessService)
    {
        base.Init(buildingData, buildingConfig, dataAccessService);
        buildingData.OnWorkersCountChanged += UpdateProgress;
        UpdateProgress(buildingData.workers);
    }

    protected override void UpdateUpgradeButton(int value)
    {
        _upgradeCostValue = buildingConfig.Upgrades.CalculateUpgradeWorkersCost(buildingData.workers);
        base.UpdateUpgradeButton(value);

        if (buildingData.workers >= 3)
        {
            _maxCount.gameObject.SetActive(true);
            _button.gameObject.SetActive(false);
        }
    }

    protected override void IncreaseLevel()
    {
        if (_currencyData.TryDecreaseCoinsCount(_upgradeCostValue))
        {
            buildingData.IncreaseWorkersCount();
            UpdateUpgradeButton(_currencyData.count);
        }
    }

    protected override void UpdateProgress(int level)
    {
        _workersCount.text = $"{level}/3";
    }
}