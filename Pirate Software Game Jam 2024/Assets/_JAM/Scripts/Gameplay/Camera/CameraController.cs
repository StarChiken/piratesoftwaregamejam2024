using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

/// <summary>
/// Handles camera movement, rotation, and zoom for the gameplay scene.
/// </summary>
public class CameraController : MyMonoBehaviour
{
    // Config properties
    private CameraConfig m_cameraConfig => ConfigManager.Instance.GetConfig<CameraConfig>();

    [Header("Camera Zoom")]
    [SerializeField] private CinemachineVirtualCamera m_cinemachineVirtualCamera;

    private float m_rotateDirection = 0f;
    private float m_currentMoveTime;
    private Vector2 m_keyboardInputDirection = Vector2.zero;
    private Vector2 m_mouseMovementRangeOffset = Vector2.zero;
    private Vector3 m_currentVelocity = Vector3.zero;
    private Rigidbody m_rb;

    private void Start()
    {
        m_rb = GetComponent<Rigidbody>();
        Vector2 mouseMovementRange = new Vector2(Screen.width - (Screen.width / m_cameraConfig.MouseEdgeMoveRangeRatio), Screen.height - (Screen.height / m_cameraConfig.MouseEdgeMoveRangeRatio));
        m_mouseMovementRangeOffset = new Vector2(mouseMovementRange.x - (Screen.width / 2), mouseMovementRange.y - (Screen.height / 2));
    }

    private void Update()
    {
        HandleCameraRotation();
        HandleCameraZoom();
        MoveCamera(m_keyboardInputDirection);
        HandleMouseCameraMove();
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, m_cameraConfig.MinXPos, m_cameraConfig.MaxXPos), transform.position.y, Mathf.Clamp(transform.position.z, m_cameraConfig.MinZPos, m_cameraConfig.MaxZPos));
    }
    
    /// <summary>
    /// Handles camera movement based on mouse position at screen edge.
    /// </summary>
    private void HandleMouseCameraMove() 
    {
        if (m_keyboardInputDirection.magnitude == 0)
        {
            Vector2 mousePositionOffset = new Vector2(Mouse.current.position.value.x - (Screen.width / 2), Mouse.current.position.value.y - (Screen.height / 2));
            if (Mathf.Abs(mousePositionOffset.x) > Mathf.Abs(m_mouseMovementRangeOffset.x) || Mathf.Abs(mousePositionOffset.y) > Mathf.Abs(m_mouseMovementRangeOffset.y))
            {
                MoveCamera(mousePositionOffset);
            }
        }
    }

    /// <summary>
    /// Moves the camera based on the given direction vector.
    /// </summary>
    private void MoveCamera(Vector2 moveVector)
    {
        Vector3 moveDir = transform.forward * moveVector.normalized.y + transform.right * moveVector.normalized.x;
        transform.position = Vector3.SmoothDamp(transform.position, transform.position + moveDir, ref m_currentVelocity, m_currentMoveTime);
    }

    /// <summary>
    /// Handles camera rotation based on input.
    /// </summary>
    private void HandleCameraRotation()
    {
        transform.eulerAngles += new Vector3(0, m_rotateDirection * m_cameraConfig.RotationSpeed * Time.deltaTime, 0);
    }

    /// <summary>
    /// Handles camera zoom based on input.
    /// </summary>
    private void HandleCameraZoom()
    {
        m_cinemachineVirtualCamera.m_Lens.FieldOfView = Mathf.Lerp(m_cinemachineVirtualCamera.m_Lens.FieldOfView, m_cameraConfig.TargetFieldOfView, Time.deltaTime * 5f);
        float t = (m_cameraConfig.TargetFieldOfView - m_cameraConfig.FieldOfViewMin) / (m_cameraConfig.FieldOfViewMax - m_cameraConfig.FieldOfViewMin);
        m_cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.y = Mathf.Lerp(m_cameraConfig.EndingZoomXOffset, m_cameraConfig.StartingZoomYOffset, t);
        m_currentMoveTime = Mathf.Lerp(m_cameraConfig.MaxZoomMoveTime, m_cameraConfig.MinZoomMoveTime, t);
    }

    /// <summary>
    /// Handles keyboard movement input.
    /// </summary>
    private void OnMovementInputDirection(InputValue inputValue)
    {
        m_keyboardInputDirection = inputValue.Get<Vector2>();
    }

    /// <summary>
    /// Handles rotation input.
    /// </summary>
    private void OnRotateInputDirection(InputValue inputValue)
    {
        m_rotateDirection = inputValue.Get<float>();
    }

    /// <summary>
    /// Handles mouse scroll input for zooming.
    /// </summary>
    private void OnMouseScroll(InputValue inputValue)
    {
        float scrollYValue = inputValue.Get<float>();
        if (scrollYValue == 1f)
        {
            m_cameraConfig.TargetFieldOfView -= 0.5f;
        }
        else if (scrollYValue == -1f)
        {
            m_cameraConfig.TargetFieldOfView += 0.5f;
        }
        m_cameraConfig.TargetFieldOfView = Mathf.Clamp(m_cameraConfig.TargetFieldOfView, m_cameraConfig.FieldOfViewMin, m_cameraConfig.FieldOfViewMax);
    }
}