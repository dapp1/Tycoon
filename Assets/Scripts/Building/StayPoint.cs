using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayPoint : MonoBehaviour
{
    [SerializeField] private bool _isOccupied;
    
    public bool IsOccupied => _isOccupied;

    public void SetOccupied(bool isOccupied) => _isOccupied = isOccupied;
}
