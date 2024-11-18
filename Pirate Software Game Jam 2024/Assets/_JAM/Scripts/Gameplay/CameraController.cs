﻿using Base.Core.Components;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

namespace Base.Gameplay
{
    public class CameraController : MyMonoBehaviour
    {
        [Header("Movement")]
        public float minZoomMoveTime;
        public float maxZoomMoveTime;
        public float rotationSpeed;

        public int mouseEdgeMoveRangeRatio;

        [Header("Min-Max Position")]
        public float maxXPos;
        public float minXPos;
        public float maxZPos;
        public float minZPos;

        [Header("Camera Zoom")]
        public float targetFieldOfView;
        public float fieldOfViewMax;
        public float fieldOfViewMin;
        public float startingZoomYOffset;
        public float endingZoomXOffset;

        public CinemachineVirtualCamera cinemachineVirtualCamera;

        private float rotateDirection = 0f;
        private float currentMoveTime;

        private Vector2 keyboardInputDirection = Vector2.zero;
        private Vector2 mouseMovementRangeOffset = Vector2.zero;

        private Vector3 currentVelocity = Vector3.zero;

        private Rigidbody rb;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();

            Vector2 mouseMovementRange = new Vector2(Screen.width - (Screen.width / mouseEdgeMoveRangeRatio), Screen.height - (Screen.height / mouseEdgeMoveRangeRatio));
            mouseMovementRangeOffset = new Vector2(mouseMovementRange.x - (Screen.width / 2), mouseMovementRange.y - (Screen.height / 2));
        }

        private void Update()
        {
            HandleCameraRotation();
            HandleCameraZoom();
            MoveCamera(keyboardInputDirection);
            HandleMouseCameraMove();
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, minXPos, maxXPos), transform.position.y, Mathf.Clamp(transform.position.z, minZPos, maxZPos));
        }
        
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

        private void MoveCamera(Vector2 moveVector)
        {
            Vector3 moveDir = transform.forward * moveVector.normalized.y + transform.right * moveVector.normalized.x;

            transform.position = Vector3.SmoothDamp(transform.position, transform.position + moveDir, ref currentVelocity, currentMoveTime);
        }

        private void HandleCameraRotation()
        {
            transform.eulerAngles += new Vector3(0, rotateDirection * rotationSpeed * Time.deltaTime, 0);
        }

        private void HandleCameraZoom()
        {
            cinemachineVirtualCamera.m_Lens.FieldOfView = Mathf.Lerp(cinemachineVirtualCamera.m_Lens.FieldOfView, targetFieldOfView, Time.deltaTime * 5f);

            float t = (targetFieldOfView - fieldOfViewMin) / (fieldOfViewMax - fieldOfViewMin);

            cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.y = Mathf.Lerp(endingZoomXOffset, startingZoomYOffset, t);
            currentMoveTime = Mathf.Lerp(maxZoomMoveTime, minZoomMoveTime, t);
        }

        private void OnMovementInputDirection(InputValue inputValue)
        {
            keyboardInputDirection = inputValue.Get<Vector2>();
        }

        private void OnRotateInputDirection(InputValue inputValue)
        {
            rotateDirection = inputValue.Get<float>();
        }

        private void OnMouseScroll(InputValue inputValue)
        {
            float scrollYValue = inputValue.Get<float>();

            if (scrollYValue == 1f)
            {
                print("Zooming In");
                targetFieldOfView -= 0.5f;
            }
            else if (scrollYValue == -1f)
            {
                print("Zooming Out");
                targetFieldOfView += 0.5f;
            }
            
            targetFieldOfView = Mathf.Clamp(targetFieldOfView, fieldOfViewMin, fieldOfViewMax);
        }
    }
}