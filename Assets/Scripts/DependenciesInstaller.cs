using System;
using System.Collections.Generic;
using UnityEngine;

public class DependenciesInstaller : MonoBehaviour
{
    [SerializeField] private List<MonoBehaviour> _initializables;

    // Register dependencies
    private void Awake()
    {
        foreach (var initializable in _initializables)
        {
            GameContext.Container.Register(initializable);
        }
    }
}

