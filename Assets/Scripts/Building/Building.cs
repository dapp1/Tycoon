using System;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] private BuildingConfig _config;
    [SerializeField] private ProgressBar _progressBar;
    [SerializeField] private GameObject[] _levelVisual;

    private int _currentLevel;
    private BuildingConfig.BuildingLevel _configByLevel;

    public event Action<int> OnLevelChanged;

    private ResourceManager _resourceManager;
    
    [Inject]
    public void Construct(ResourceManager resourceManager)
    {
        _resourceManager = resourceManager;
    }
    
    [ContextMenu("Init")]
    public void Init()
    {
        GameContext.Container.InjectDependencies(this);
        
        _configByLevel = _config.GetLevelConfig(_currentLevel);
        _progressBar.Initialize(_configByLevel.ProductionTime);
        
        for (var i = 0; i < _levelVisual.Length; i++)
        {
            if (_currentLevel < 0 || _currentLevel >= _levelVisual.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(_currentLevel), "Level is out of range.");
            }
            
            _levelVisual[i].SetActive(i == _currentLevel);
        }

        _progressBar.OnProgressComplete += () => _resourceManager.GetResource(CurrencyType.Coin)
            .AddCurrencyValue(_configByLevel.ProductionCount);
    }
    
    [ContextMenu("Producing")]
    public void StartProducing()
    {
        _progressBar.StartFilling();
    }
}