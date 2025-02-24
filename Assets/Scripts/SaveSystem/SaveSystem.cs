using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace SaveSystem
{
    public static class SaveSystem
    {
        private static readonly string path = Application.persistentDataPath + "/save.json";
        
        public static void Initialize()
        {
            if (File.Exists(path))
            {
                LoadData();
            }
            else
            {
                SaveData data = new SaveData
                {
                    currencyData = new List<CurrencyData>
                    {
                        new() { type = "coins", count = 10000 },
                        new() { type = "gems", count = 100 }
                    },
                    buildingData = new List<BuildingInfoData>()
                };

                SaveData(data);
            }
        }

        public static void SaveData(SaveData data)
        {
            string json = JsonUtility.ToJson(data, true);
            File.WriteAllText(path, json);
        }

        public static SaveData LoadData()
        {
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                return JsonUtility.FromJson<SaveData>(json);
            }

            return null;
        }
    }
}