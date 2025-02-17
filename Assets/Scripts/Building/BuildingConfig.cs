using System;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(BuildingConfig), menuName = "Configs/" + nameof(BuildingConfig))]
public class BuildingConfig : ScriptableObject
{
    [SerializeField] private BuildingLevel[] _levels;
    
    public BuildingLevel GetLevelConfig(int level)
    {
        if (level < 0 || level >= _levels.Length)
        {
            throw new ArgumentOutOfRangeException(nameof(level), "Level is out of range.");
        }

        return _levels[level];
    }
    
    [Serializable]
    public struct BuildingLevel
    {
        [SerializeField] private int _productionTime;
        [SerializeField] private int _productionCount;

        public int ProductionTime => _productionTime;
        public int ProductionCount => _productionCount;
    }
}
