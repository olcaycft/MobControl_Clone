using System;
using System.Collections;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    private Vector2 inputDrag;
    private Vector2 previousMousePosition;

    [SerializeField] private Transform sideMovementRoot;
    private float sideMovementSensitivity => SettingsManager.GameSettings.sideMovementSensitivity;

    [SerializeField] private Transform cannonRightLimit;
    [SerializeField] private Transform cannonLeftLimit;

    private float spawnInterval => SettingsManager.GameSettings.spawnIntervalForPlayer;
    private float instantiationTimer;
    private float cannonRightLimitX => cannonRightLimit.localPosition.x;
    private float cannonLeftLimitX => cannonLeftLimit.localPosition.x;

    [SerializeField] private int numberOfPlayerThrown = 0;

    private void Awake()
    {
        instantiationTimer = spawnInterval;
    }

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
            var deltaMouse = (Vector2) Input.mousePosition - previousMousePosition;
            inputDrag = deltaMouse;
            previousMousePosition = Input.mousePosition;

            //here is for little blue ones

            instantiationTimer -= Time.deltaTime;
            if (instantiationTimer <= Time.deltaTime)
            {
                SpawnRoutinePlayer();
                instantiationTimer = spawnInterval;
                numberOfPlayerThrown++;
            }
        }
        else
        {
            inputDrag = Vector2.zero;
            instantiationTimer = spawnInterval;
            //here is for big yellow one if i can add
            if (numberOfPlayerThrown>=20)
            {
                SpawnRoutineGiant();
                numberOfPlayerThrown = 0;
            }
        }
    }

    private void SideMovement()
    {
        var localPos = sideMovementRoot.localPosition;
        localPos += Vector3.right * inputDrag.x * sideMovementSensitivity;
        localPos.x = Mathf.Clamp(localPos.x, cannonLeftLimitX, cannonRightLimitX);
        sideMovementRoot.localPosition = localPos;
    }

    private void SpawnRoutinePlayer()
    {
        var localPos = sideMovementRoot.position;
        localPos.z += 1.2f;
        ObjectPooler.Instance.SpawnFromPool("Player", localPos, sideMovementRoot.transform.rotation);
    }
    private void SpawnRoutineGiant()
    {
        var localPos = sideMovementRoot.position;
        localPos.z += 1.2f;
        ObjectPooler.Instance.SpawnFromPool("Giant", localPos, sideMovementRoot.transform.rotation);
    }
}