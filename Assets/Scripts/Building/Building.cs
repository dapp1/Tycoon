using System;
using SaveSystem;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] private BuildingConfig _config;
    [SerializeField] private ProgressBar _progressBar;
    [SerializeField] private GameObject[] _levelVisual;
    [SerializeField] private ClickableObject _clickHandler;
    
    private int _currentWorkersCount;

    private DataAccessService _accessService;
    private UpgradeManager _upgradeManager;
    private BuildingInfoData _buildingInfoData;
    private CurrencyData _currencyData;
    
    private void Awake() => GameContext.Container.InjectDependencies(this);

    [Inject]
    public void Construct(DataAccessService accessService, UpgradeManager upgradeManager)
    {
        _accessService = accessService;
        _upgradeManager = upgradeManager;
        
        _currencyData = accessService.GetCurrencyData(DataStrings.Currencies.Coins);
        _buildingInfoData = accessService.GetBuildingData(_config.BuildingID);
        _buildingInfoData.OnWorkersCountChanged += UpdateWorkersCount;
        _buildingInfoData.OnProduceSpeedLevelChanged += ChangeProductionSpeed;
        
        UpdateWorkersCount(_buildingInfoData.workers);

        _progressBar.Initialize(_config.CalculateSpeed(_buildingInfoData.produceSpeedLevel));
        
        _clickHandler.OnClick.AddListener(InitUpgradeWindow);
        _progressBar.OnProgressComplete += IncreaseCoins;
        _progressBar.OnProgressComplete += StartProducing;
    }

    private void Start()
    {
        StartProducing();
    }

    private void IncreaseCoins()
    {
        int value = _config.CalculateRevenue(_buildingInfoData.revenueLevel);
        _currencyData.IncreaseCoinsCount(value);
    }
    
    private void InitUpgradeWindow() => _upgradeManager.InitUpgradeWindow(_buildingInfoData, _config, _accessService);

    private void UpdateWorkersCount(int workersCount)
    {
        for (var i = 0; i < workersCount + 1; i++)
        {
            if (_currentWorkersCount < 0 || _currentWorkersCount >= _levelVisual.Length)
                throw new ArgumentOutOfRangeException(nameof(_currentWorkersCount), "Level is out of range.");
        
            _levelVisual[i].SetActive(true);
        }
    }
    
    private void ChangeProductionSpeed(int level) 
        => _progressBar.Initialize(_config.CalculateSpeed(_buildingInfoData.produceSpeedLevel));
    
    public void StartProducing()
    {
        _progressBar.StartFilling();
    }
}