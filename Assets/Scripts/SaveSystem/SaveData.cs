using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class SaveData
{
    public List<CurrencyData> currencyData = new List<CurrencyData>();
    public List<BuildingInfoData> buildingData = new List<BuildingInfoData>();

    public CurrencyData GetCurrencyData(string id) => currencyData.FirstOrDefault(c => c.type == id);
    
    public BuildingInfoData GetBuildingData(string id) => buildingData.FirstOrDefault(b => b.id == id);
}

[Serializable]
public class CurrencyData
{
    public string type;
    public int count;

    public event UnityAction<int> OnChange;
    
    public void IncreaseCoinsCount(int count)
    {
        this.count += count;
        OnChange?.Invoke(this.count);
    }

    public bool TryDecreaseCoinsCount(int count)
    {
        if (this.count >= count)
        {
            this.count -= count;
            OnChange?.Invoke(this.count);
            return true;
        }
        return false;
    }
}

[Serializable]
public class BuildingInfoData
{
    public string id;
    public int workers;
    public int revenueLevel;
    public int produceSpeedLevel;
    
    public event UnityAction<int> OnWorkersCountChanged; 
    public event UnityAction<int> OnRevenueLevelChanged; 
    public event UnityAction<int> OnProduceSpeedLevelChanged;

    public void IncreaseWorkersCount(int count = 1)
    {
        workers += count;
        OnWorkersCountChanged?.Invoke(workers);
    }
    
    public void IncreaseRevenueLevel(int level = 1)
    {
        revenueLevel += level;
        OnRevenueLevelChanged?.Invoke(revenueLevel);
    }
    
    public void IncreaseProduceSpeedLevel(int level = 1)
    {
        produceSpeedLevel += level;
        OnProduceSpeedLevelChanged?.Invoke(produceSpeedLevel);
    }
}