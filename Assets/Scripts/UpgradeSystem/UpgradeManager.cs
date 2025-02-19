using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] private Canvas _upgradeWindow;
    [SerializeField] private RevenueUpgrade _revenueUpgrade;
    [SerializeField] private WorkersInBuildingUpgrade _workersProgress;
    [SerializeField] private ProductionTimeUpgrade _productionTimeUpgrade;
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
        _upgradeWindow.gameObject.SetActive(true);
    }

    private void CloseWindow()
    {
        _revenueUpgrade.OnClose();
        _upgradeWindow.gameObject.SetActive(false);
    }
}
