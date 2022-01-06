using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    private Vector2 inputDrag;
    private Vector2 previousMousePosition;

    [SerializeField] private Transform sideMovementRoot;
    [SerializeField] private float sideMovementSensitivity = 0.5f;

    [SerializeField] private Transform cannonRightLimit;
    [SerializeField] private Transform cannonLeftLimit;
    [SerializeField] private float scaleTime = 0.03f;

    private float cannonRightLimitX => cannonRightLimit.localPosition.x;
    private float cannonLeftLimitX => cannonLeftLimit.localPosition.x;

    private void Update()
    {
        HandleInput();
        SideMovement();
    }

    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            previousMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            var deltaMouse = (Vector2)Input.mousePosition - previousMousePosition;
            inputDrag = deltaMouse;
            previousMousePosition = Input.mousePosition;
        }
        else
        {
            inputDrag = Vector2.zero;
        }
    }

    private void SideMovement()
    {
        var localPos = sideMovementRoot.localPosition;
        localPos+=Vector3.right*inputDrag.x*sideMovementSensitivity;
        localPos.x = Mathf.Clamp(localPos.x, cannonLeftLimitX, cannonRightLimitX);
        sideMovementRoot.localPosition = localPos;
        
    }
}