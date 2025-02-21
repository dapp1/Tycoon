using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Links")] 
    [SerializeField] private Camera _camera;
    
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 10f;     
    [SerializeField] private float scrollSpeed = 10f;
    [SerializeField] private float minZoom = 5f;
    [SerializeField] private float maxZoom = 50f;
    
    [Header("Borders")]
    [SerializeField] private float xMin = -1000f;
    [SerializeField] private float xMax = 1150f; 
    [SerializeField] private float zMin = -1150f; 
    [SerializeField] private float zMax = 1150f; 
    
    [Header("Camera Rotation")]
    [SerializeField] private float rotationSpeed = 50f; 

    private void FixedUpdate()
    {
        MoveCamera();
        ZoomCamera();
        RotateCamera();
    }

    private void MoveCamera()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        
        Vector3 moveDirection = new Vector3(-horizontalInput, 0, -verticalInput) * moveSpeed * Time.fixedDeltaTime;
        
        transform.Translate(moveDirection, Space.World);
        
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, xMin, xMax);
        clampedPosition.z = Mathf.Clamp(clampedPosition.z, zMin, zMax);
        transform.position = clampedPosition;
    }


    private void ZoomCamera()
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        _camera.fieldOfView -= scrollInput * scrollSpeed;
        
        _camera.fieldOfView = Mathf.Clamp(_camera.fieldOfView, minZoom, maxZoom);
    }

    private void RotateCamera()
    {
        if (Input.GetMouseButton(1))
        {
            float rotationInput = Input.GetAxis("Mouse X");
            transform.Rotate(0, rotationInput * rotationSpeed * Time.deltaTime, 0);
        }
    }
}
