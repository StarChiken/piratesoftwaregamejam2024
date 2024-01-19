using Base.Core.Components;
using UnityEngine;

namespace Base.Gameplay.UI
{
    public class CameraController : MyMonoBehaviour
    {
        public float moveSpeed = 50f;
        public float rotationSpeed = 100f;
        private bool dragPanMoveActive;
        private Vector2 lastMousePosition;
        private void Update()
        {
            HandleKeyboardCameraMove();
            HandleMouseCameraMove();
            HandleCameraRotation();
        }
        
        private void HandleKeyboardCameraMove() 
        {
            Vector3 inputDirection = new Vector3(0, 0, 0);

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode. UpArrow))
            {
                inputDirection.z = +1f;
            }

            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode. DownArrow))
            {
                inputDirection.z = -1f;
            }

            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode. LeftArrow))
            {
                inputDirection.x = -1f;
            }

            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode. RightArrow))
            {
                inputDirection.x = +1f;
            }

            Vector3 moveDirection = transform.forward * inputDirection.z + transform.right * inputDirection.x;

            
            transform.position += moveDirection * moveSpeed * Time.deltaTime;
        }
        
        private void HandleMouseCameraMove() 
        {
            Vector3 inputDirection = new Vector3(0, 0, 0);

            if (Input.GetMouseButtonDown(1)) 
            {
                dragPanMoveActive = true;
                lastMousePosition = Input.mousePosition;
            }
            
            if (Input.GetMouseButtonUp(1)) 
            {
                dragPanMoveActive = false;
            }

            if (dragPanMoveActive) 
            {
                Vector2 mouseMovementDelta = (Vector2)Input.mousePosition - lastMousePosition;

                float panSpeedMulti = 1f;
                inputDirection.x = mouseMovementDelta.x * panSpeedMulti;
                inputDirection.z = mouseMovementDelta.y * panSpeedMulti;

                lastMousePosition = Input.mousePosition;
            }

            Vector3 moveDir = transform.forward * inputDirection.z + transform.right * inputDirection.x;
            
            transform.position += moveDir * moveSpeed * Time.deltaTime;
        }
        
        private void HandleCameraRotation() 
        {
            float rotateDirection = 0f;
            
            if (Input.GetKey(KeyCode.Q))
            {
                rotateDirection = +1f;
            }

            if (Input.GetKey(KeyCode.E))
            {
                rotateDirection = -1f;
            }
            
            transform.eulerAngles += new Vector3(0, rotateDirection * rotationSpeed * Time.deltaTime, 0);
        }
    }
}