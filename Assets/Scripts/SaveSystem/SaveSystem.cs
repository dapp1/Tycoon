using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace SaveSystem
{
    public static class SaveSystem
    {
        private static readonly string path = Application.persistentDataPath + "/save.json";
        
        public static SaveData Initialize()
        {
            if (File.Exists(path))
            {
                Debug.Log(Application.persistentDataPath + "/save.json");
                return LoadData();
            }
            else
            {
                SaveData data = new SaveData
                {
                    currencyData = new List<CurrencyData>
                    {
                        new() { type = "coins", count = 0 },
                        new() { type = "gems", count = 0 }
                    },
                    buildingData = new List<BuildingInfoData>()
                };

                SaveData(data);
                Debug.Log(Application.persistentDataPath + "/save.json");
                return data;
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