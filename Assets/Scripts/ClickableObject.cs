using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(BoxCollider))]
public class ClickableObject : MonoBehaviour
{
    public UnityEvent OnClick;
    
    private void OnMouseUp()
    {
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            return;

        OnClick?.Invoke();
    }
}