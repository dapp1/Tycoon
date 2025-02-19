using System.Linq;
using UnityEngine;

public class DataAccessService : MonoBehaviour
{
    private SaveData _data;

    public void Awake()
    {
        _data = SaveSystem.SaveSystem.LoadData();
    }
    
    public CurrencyData GetCurrencyData(string id) => _data.currencyData.FirstOrDefault(c => c.type == id);
    
    public BuildingInfoData GetBuildingData(string id)
    {
        BuildingInfoData buildData = _data.buildingData.FirstOrDefault(b => b.id == id);

        if (buildData == null)
        {
            buildData = new()
            {
                id = id,
                workers = 1,
                revenueLevel = 1,
                produceSpeedLevel = 1
            };
            
            _data.buildingData.Add(buildData);
            return buildData;
        }

        return buildData;
    }

    private void OnApplicationQuit() =>
        SaveSystem.SaveSystem.SaveData(_data);
    
    private void OnDestroy() =>
        SaveSystem.SaveSystem.SaveData(_data);
}
