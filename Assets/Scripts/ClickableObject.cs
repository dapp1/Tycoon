using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(BoxCollider))]
public class ClickableObject : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    
    public UnityEvent OnClick;

    private void OnMouseUp()
    {
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            return;
        
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.gameObject == gameObject)
            {
                OnClick?.Invoke();
            }
        }
    }
}