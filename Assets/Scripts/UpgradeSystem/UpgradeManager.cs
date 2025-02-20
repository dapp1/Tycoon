using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] private Canvas _upgradeWindow;
    [SerializeField] private RevenueUpgrade _revenueUpgrade;
    [SerializeField] private WorkersInBuildingUpgrade _workersProgress;
    [SerializeField] private ProductionTimeUpgrade _productionTimeUpgrade;
    [SerializeField] private BuildingUIInfo _buildingUI;
    [SerializeField] private Button _closeButton;
    
    private void Awake()
    {
        GameContext.InjectDependencies(this);
        _closeButton.onClick.AddListener(CloseWindow);
    }

    public void InitUpgradeWindow(BuildingInfoData buildingData, BuildingConfig buildingConfig, DataAccessService accessService)
    {
        _revenueUpgrade.Init(buildingData, buildingConfig, accessService);
        _workersProgress.Init(buildingData, buildingConfig, accessService);
        _productionTimeUpgrade.Init(buildingData, buildingConfig, accessService);
        _buildingUI.Init(buildingConfig);
        _upgradeWindow.gameObject.SetActive(true);
    }

    private void CloseWindow()
    {
        _revenueUpgrade.OnClose();
        _upgradeWindow.gameObject.SetActive(false);
    }

    [Serializable]
    private class BuildingUIInfo
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _description;
        [SerializeField] private TextMeshProUGUI _productName;

        public void Init(BuildingConfig config)
        {
            _icon.sprite = config.UIValues.Icon;
            _name.text = config.UIValues.BuildingName;
            _description.text = config.UIValues.BuildingDescription;
            _productName.text = config.UIValues.ProductName;
        }
    }
}
