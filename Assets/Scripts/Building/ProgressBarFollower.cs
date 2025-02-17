using UnityEngine;

public class ProgressBarFollower : MonoBehaviour
{
    [Header("Main")]
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Transform _targetTransform;
    [SerializeField] private RectTransform _barTransform;

    [Header("Settings")]
    [SerializeField] private float additivePositionX;
    [SerializeField] private float additivePositionY;

    private void FixedUpdate()
    {
        if (!_targetTransform || !_canvas) return;
        
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(_targetTransform.position);

        // Устанавливаем позицию прогресс-бара относительно Canvas
        _barTransform.position = screenPosition + new Vector3(additivePositionX, additivePositionY, 0);
    }
}