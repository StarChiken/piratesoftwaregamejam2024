using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MiracleCursorObject : MonoBehaviour
{
    public Vector3 offset;

    private bool followCursor = false;

    private void Start()
    {
        transform.position = new Vector3(-300, -300, -300);
    }

    public void SetFollowCursor(bool _followCursor)
    {
        followCursor = _followCursor;
        if (!_followCursor)
        {
            transform.position = new Vector3(-300, -300, -300);
        }
    }

    private void Update()
    {
        if (followCursor)
        {
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                SetFollowCursor(false);
            }
            else
            {
                Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.value);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 300))
                {
                    transform.position = hit.point + offset;
                }
            }
        }
    }
}
