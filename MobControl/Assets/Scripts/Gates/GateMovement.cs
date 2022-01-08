using UnityEngine;
using Random = UnityEngine.Random;

public class GateMovement : MonoBehaviour
{
    [SerializeField] private Transform sideMovementRoot;
    [SerializeField] private Transform gateRightLimit;
    [SerializeField] private Transform gateLeftLimit;
    private float gateRightLimitX => gateRightLimit.localPosition.x;
    private float gateLeftLimitX => gateLeftLimit.localPosition.x;
    private float sideMovementSensitivity => SettingsManager.GameSettings.gateSideMovementSensitivity;

    private int movementSide;
    [SerializeField] private bool goLeft;
    

    private void Awake()
    {
        movementSide = Random.Range(0, 2);
        if (movementSide == 0)
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