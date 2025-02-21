using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Image _fillImage;

    [Header("Settings")] 
    [SerializeField] private float _updateInterval = 0.25f; 
    
    private float _progress;
    private float _totalDuration;
    private float _elapsedTime;
    
    public event Action OnProgressComplete;
    
    public void Initialize(float duration)
    {
        _totalDuration = duration;
    }
    
    public void StartFilling()
    {
        StopAllCoroutines();
        StartCoroutine(FillProgressBar());
    }

    private IEnumerator FillProgressBar()
    {
        yield return null;
        
        _fillImage.gameObject.SetActive(true);
        _elapsedTime = 0;
        _progress = 0;
        
        while (_elapsedTime < _totalDuration)
        {
            yield return new WaitForSeconds(_updateInterval);
            _elapsedTime += _updateInterval;
            UpdateProgressBar();
        }

        _elapsedTime = _totalDuration;
        UpdateProgressBar();
        OnProgressComplete?.Invoke();
    }

    private void UpdateProgressBar()
    {
        _progress = _elapsedTime / _totalDuration;
        _fillImage.fillAmount = _progress;
    }
}