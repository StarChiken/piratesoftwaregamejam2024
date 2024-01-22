using Base.Core.Components;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Base.Gameplay.UI
{
    public class CameraController : MyMonoBehaviour
    {
        [Header("Movement")]
        public float moveSpeed;
        public float rotationSpeed;

        public int mouseEdgeMoveRangeRatio;

        [Header("Min-Max Position")]
        public float maxXPos;
        public float minXPos;
        public float maxZPos;
        public float minZPos;

        private float rotateDirection = 0f;

        private Vector2 keyboardInputDirection = Vector2.zero;
        private Vector2 mouseMovementRangeOffset = Vector2.zero;

        private void Start()
        {
            Vector2 mouseMovementRange = new Vector2(Screen.width - (Screen.width / mouseEdgeMoveRangeRatio), Screen.height - (Screen.height / mouseEdgeMoveRangeRatio));
            mouseMovementRangeOffset = new Vector2(mouseMovementRange.x - (Screen.width / 2), mouseMovementRange.y - (Screen.height / 2));
        }

        private void Update()
        {
            HandleKeyboardCameraMove();
            HandleMouseCameraMove();
            HandleCameraRotation();

            transform.position = new Vector3(Mathf.Clamp(transform.position.x, minXPos, maxXPos), transform.position.y, Mathf.Clamp(transform.position.z, minZPos, maxZPos));
        }
        
        private void HandleKeyboardCameraMove() 
        {
            Vector3 moveDirection = transform.forward * keyboardInputDirection.y + transform.right * keyboardInputDirection.x;
            
            transform.position += moveDirection * moveSpeed * Time.deltaTime;
        }
        
        private void HandleMouseCameraMove() 
        {
            Vector2 mousePositionOffset = new Vector2(Mouse.current.position.value.x - (Screen.width / 2), Mouse.current.position.value.y - (Screen.height / 2));

            if (Mathf.Abs(mousePositionOffset.x) > Mathf.Abs(mouseMovementRangeOffset.x) || Mathf.Abs(mousePositionOffset.y) > Mathf.Abs(mouseMovementRangeOffset.y))
            {
                Vector3 moveDir = transform.forward * mousePositionOffset.normalized.y + transform.right * mousePositionOffset.normalized.x;
                transform.position += moveDir * moveSpeed * Time.deltaTime;
            }
        }
        
        private void HandleCameraRotation() 
        {
            transform.eulerAngles += new Vector3(0, rotateDirection * rotationSpeed * Time.deltaTime, 0);
        }

        private void OnMovementInputDirection(InputValue inputValue)
        {
            keyboardInputDirection = inputValue.Get<Vector2>();
        }

        private void OnRotateInputDirection(InputValue inputValue)
        {
            rotateDirection = inputValue.Get<float>();
        }
    }
}