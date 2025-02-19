using System;
using System.Linq;
using SaveSystem;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ResourceManager : MonoBehaviour
{
    [SerializeField] private Currency[] _currency;
    
    [Inject] private DataAccessService _accessService;

    private void Awake()
    {
        GameContext.InjectDependencies(this);
    }

    public void Start()
    {
        foreach (Currency currency in _currency)
        {
            CurrencyData data = _accessService.GetCurrencyData(currency.Type);
            currency.Init(data);
        }
    }
    
    public Currency GetResource(string id)
    {
        return _currency.FirstOrDefault(currency => currency.Type == id);
    }
}

[Serializable]
public class Currency
{
    [SerializeField] private string _type;
    [SerializeField] private TextMeshProUGUI _textCount;
    
    public string Type => _type;
    private CurrencyData _currencyData;
    
    public void Init(CurrencyData currencyData)
    {
        currencyData.OnChange += UpdateText;
        UpdateText(currencyData.count);
    }

    private void UpdateText(int count)
    {
        if (_textCount != null)
        {
            _textCount.text = count.ToString();
        }
    }
}