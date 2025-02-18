using System;
using System.Linq;
using TMPro;
using UnityEngine;

public enum CurrencyType { Coin, Gem }

public class ResourceManager : MonoBehaviour
{
    [SerializeField] private Currency[] _currency;
    
    public Currency GetResource(CurrencyType type)
    {
        return _currency.FirstOrDefault(currency => currency.Type == type);
    }
}

[Serializable]
public class Currency
{
    [SerializeField] private CurrencyType _type;
    [SerializeField] private TextMeshProUGUI _textCount;

    private int _currentCount;
    
    public event Action<int> OnCountChanged;
    
    public int Count => _currentCount;
    public CurrencyType Type => _type;

    public void AddCurrencyValue(int value)
    {
        _currentCount += value;
        UpdateText();
        OnCountChanged?.Invoke(_currentCount);
    }

    private void UpdateText()
    {
        if (_textCount != null)
            _textCount.text = _currentCount.ToString();
    }
}