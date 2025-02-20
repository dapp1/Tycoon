using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform cameraTransform;

    [SerializeField] private float _camSpeed = 1f; //Speed of the camera
    [SerializeField] private float _camSpeedFast = 5f; //Speed of the camera while holding "Fast camera movement button"

    [SerializeField] private float _camMovementSpeed = 1f;
    [SerializeField] private float _camSmoothness = 10f;

    [SerializeField] private float _camRotationAmount = 1f;
    [SerializeField] private float _camBorderMovement = 5f;

    [SerializeField] private float _maxCamZoom = 30f;
    [SerializeField] private float _minCamZoom = 100f;

    [SerializeField] private float _minZCamMovement = 100f;
    [SerializeField] private float _maxZCamMovement = 900f;
    [SerializeField] private float _minXCamMovement = 100f;
    [SerializeField] private float _maxXCamMovement = 900f;

    [SerializeField] private bool cursorVisible = true;

    public Vector3 zoomAmount;

    public Vector3 newPosition;
    public Quaternion newRotation;
    public Vector3 newZoom;

    //MouseMovement
    public Vector3 rotateStartPosition;
    public Vector3 rotateCurrentPosition;

    Vector2 pos1;
    Vector2 pos2;

    // Start is called before the first frame update
    void Start()
    {
        newPosition = transform.position;
        newRotation = transform.rotation;
        newZoom = cameraTransform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovementInput();
        HandleMouseInput();
    }

    private void HandleMouseInput()
    {
        
    }

    void HandleMovementInput()
    {
        
    }
}
