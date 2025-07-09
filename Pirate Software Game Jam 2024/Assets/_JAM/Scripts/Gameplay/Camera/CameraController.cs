using Base.Core.Components;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using Base.Core.Config;

namespace Base.Gameplay
{
    /// <summary>
    /// Handles camera movement, rotation, and zoom for the gameplay scene.
    /// </summary>
    public class CameraController : MyMonoBehaviour
    {
        // Config properties
        private CameraConfig CameraConfig => ConfigManager.Instance.GetConfig<CameraConfig>();

        [Header("Camera Zoom")]
        [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;

        private float rotateDirection = 0f;
        private float currentMoveTime;
        private Vector2 keyboardInputDirection = Vector2.zero;
        private Vector2 mouseMovementRangeOffset = Vector2.zero;
        private Vector3 currentVelocity = Vector3.zero;
        private Rigidbody rb;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            Vector2 mouseMovementRange = new Vector2(Screen.width - (Screen.width / CameraConfig.MouseEdgeMoveRangeRatio), Screen.height - (Screen.height / CameraConfig.MouseEdgeMoveRangeRatio));
            mouseMovementRangeOffset = new Vector2(mouseMovementRange.x - (Screen.width / 2), mouseMovementRange.y - (Screen.height / 2));
        }

        private void Update()
        {
            HandleCameraRotation();
            HandleCameraZoom();
            MoveCamera(keyboardInputDirection);
            HandleMouseCameraMove();
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, CameraConfig.MinXPos, CameraConfig.MaxXPos), transform.position.y, Mathf.Clamp(transform.position.z, CameraConfig.MinZPos, CameraConfig.MaxZPos));
        }
        
        /// <summary>
        /// Handles camera movement based on mouse position at screen edge.
        /// </summary>
        private void HandleMouseCameraMove() 
        {
            if (keyboardInputDirection.magnitude == 0)
            {
                Vector2 mousePositionOffset = new Vector2(Mouse.current.position.value.x - (Screen.width / 2), Mouse.current.position.value.y - (Screen.height / 2));
                if (Mathf.Abs(mousePositionOffset.x) > Mathf.Abs(mouseMovementRangeOffset.x) || Mathf.Abs(mousePositionOffset.y) > Mathf.Abs(mouseMovementRangeOffset.y))
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
            transform.position = Vector3.SmoothDamp(transform.position, transform.position + moveDir, ref currentVelocity, currentMoveTime);
        }

        /// <summary>
        /// Handles camera rotation based on input.
        /// </summary>
        private void HandleCameraRotation()
        {
            transform.eulerAngles += new Vector3(0, rotateDirection * CameraConfig.RotationSpeed * Time.deltaTime, 0);
        }

        /// <summary>
        /// Handles camera zoom based on input.
        /// </summary>
        private void HandleCameraZoom()
        {
            cinemachineVirtualCamera.m_Lens.FieldOfView = Mathf.Lerp(cinemachineVirtualCamera.m_Lens.FieldOfView, CameraConfig.TargetFieldOfView, Time.deltaTime * 5f);
            float t = (CameraConfig.TargetFieldOfView - CameraConfig.FieldOfViewMin) / (CameraConfig.FieldOfViewMax - CameraConfig.FieldOfViewMin);
            cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.y = Mathf.Lerp(CameraConfig.EndingZoomXOffset, CameraConfig.StartingZoomYOffset, t);
            currentMoveTime = Mathf.Lerp(CameraConfig.MaxZoomMoveTime, CameraConfig.MinZoomMoveTime, t);
        }

        /// <summary>
        /// Handles keyboard movement input.
        /// </summary>
        private void OnMovementInputDirection(InputValue inputValue)
        {
            keyboardInputDirection = inputValue.Get<Vector2>();
        }

        /// <summary>
        /// Handles rotation input.
        /// </summary>
        private void OnRotateInputDirection(InputValue inputValue)
        {
            rotateDirection = inputValue.Get<float>();
        }

        /// <summary>
        /// Handles mouse scroll input for zooming.
        /// </summary>
        private void OnMouseScroll(InputValue inputValue)
        {
            float scrollYValue = inputValue.Get<float>();
            if (scrollYValue == 1f)
            {
                CameraConfig.TargetFieldOfView -= 0.5f;
            }
            else if (scrollYValue == -1f)
            {
                CameraConfig.TargetFieldOfView += 0.5f;
            }
            CameraConfig.TargetFieldOfView = Mathf.Clamp(CameraConfig.TargetFieldOfView, CameraConfig.FieldOfViewMin, CameraConfig.FieldOfViewMax);
        }
    }
}