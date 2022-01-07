using System;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Random = UnityEngine.Random;

public class GateMovement : MonoBehaviour
{
    private int movemetnSide;
    [SerializeField] private bool goLeft;
    [SerializeField] private Transform sideMovementRoot;
    private float sideMovementSensitivity = 3f;

    [SerializeField] private Transform gateRightLimit;
    [SerializeField] private Transform gateLeftLimit;

    private float gateRightLimitX => gateRightLimit.localPosition.x;
    private float gateLeftLimitX => gateLeftLimit.localPosition.x;

    private void Awake()
    {
        movemetnSide = Random.Range(0, 2);
        if (movemetnSide == 0)
        {
            goLeft = true;
        }
        else
        {
            goLeft = false;
        }
    }

    private void Update()
    {
        if (goLeft)
        {
            SideMovementLeft();
        }
        else if (!goLeft)
        {
            SideMovementRight();
        }
    }

    private void SideMovementRight()
    {
        var localPos = sideMovementRoot.localPosition;
        if (localPos.x < gateRightLimitX)
        {
            localPos += Vector3.right * sideMovementSensitivity * Time.deltaTime;
            localPos.x = Mathf.Clamp(localPos.x, gateLeftLimitX, gateRightLimitX);
            sideMovementRoot.localPosition = localPos;
        }
        else
        {
            goLeft = true;
        }
    }

    private void SideMovementLeft()
    {
        var localPos = sideMovementRoot.localPosition;
        if (localPos.x > gateLeftLimitX)
        {
            localPos -= Vector3.right * sideMovementSensitivity * Time.deltaTime;
            localPos.x = Mathf.Clamp(localPos.x, gateLeftLimitX, gateRightLimitX);
            sideMovementRoot.localPosition = localPos;
        }
        else
        {
            goLeft = false;
        }
    }
}