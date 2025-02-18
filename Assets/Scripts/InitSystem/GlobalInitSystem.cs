using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class GlobalInitSystem : MonoBehaviour
{
    [SerializeField][RequireInterface(typeof(IInitializable))]
    private List<Object> _listeners;

    private void Start()
    {
        foreach (var o in _listeners)
        {
            
        }
    }

    public void AddToInitialize(IInitializable init)
    {
        
    }
}
