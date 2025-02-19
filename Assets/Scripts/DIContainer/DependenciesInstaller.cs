using System;
using System.Collections.Generic;
using UnityEngine;

public class DependenciesInstaller : MonoBehaviour
{
    [SerializeField] private List<MonoBehaviour> _initializables;

    private SaveData _saveData;
    // Register dependencies
    private void Awake()
    {
        SaveSystem.SaveSystem.Initialize();
        
        foreach (var initializable in _initializables)
        {
            GameContext.Container.Register(initializable);
        }
    }

    private void OnApplicationQuit()
    {
        SaveSystem.SaveSystem.SaveData(_saveData);
    }
}

