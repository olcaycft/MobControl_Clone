using System;
using System.Collections;
using UnityEngine;

public class Cannon : MonoSingleton<Cannon>
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
    private int giantCount = 20;
    public event Action<float>OnProgressChange; 
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
                var localPos = sideMovementRoot.position;
                var rotation = sideMovementRoot.transform.rotation;
                GameManager.Instance.SpawnRequest("Player", localPos, rotation,1);
                
                instantiationTimer = spawnInterval;
                numberOfPlayerThrown++;
                
                float currentThrownPct = (float) numberOfPlayerThrown / (float) giantCount;
                OnProgressChange?.Invoke(currentThrownPct);

            }
        }
        else
        {
            inputDrag = Vector2.zero;
            instantiationTimer = spawnInterval;
            //here is for big yellow one if i can add
            if (numberOfPlayerThrown>=giantCount)
            {
                var localPos = sideMovementRoot.position;
                var rotation = sideMovementRoot.transform.rotation;
                GameManager.Instance.SpawnRequest("Giant", localPos, rotation,1);
                
                numberOfPlayerThrown = 0;
                float currentThrownPct = (float) numberOfPlayerThrown / (float) giantCount;
                OnProgressChange?.Invoke(currentThrownPct);
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
}