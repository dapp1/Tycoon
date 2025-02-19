using System;
using SaveSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public abstract class UpgradeModule
{
    [Header("Button")]
    [SerializeField] protected Button _button;
    [SerializeField] protected TextMeshProUGUI _upgradeCost;

    protected int _upgradeCostValue;
    
    protected BuildingInfoData buildingData;
    protected BuildingConfig buildingConfig;
    protected DataAccessService dataAccessService;

    protected CurrencyData _currencyData;

    public virtual void Init(BuildingInfoData buildingData, BuildingConfig buildingConfig, DataAccessService dataAccessService)
    {
        this.buildingData = buildingData;
        this.buildingConfig = buildingConfig;
        this.dataAccessService = dataAccessService;

        _currencyData = dataAccessService.GetCurrencyData(DataStrings.Currencies.Coins);
        _currencyData.OnChange += UpdateUpgradeButton;
        _button.onClick.AddListener(IncreaseLevel);
        
        UpdateUpgradeButton(_currencyData.count);
    }

    protected virtual void UpdateUpgradeButton(int value)
    {
        string color = value > _upgradeCostValue ? "white" : "red";
        
        _button.interactable = value > _upgradeCostValue;
        _upgradeCost.text = $"<sprite name=Coin> <color={color}>{_upgradeCostValue}</color>";
    }

    public virtual void OnClose()
    {
        _button.onClick.RemoveAllListeners();
        _currencyData.OnChange -= UpdateUpgradeButton;
    }

    protected abstract void IncreaseLevel();
    protected abstract void UpdateProgress(int level);
}