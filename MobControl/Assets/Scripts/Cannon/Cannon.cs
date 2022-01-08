using UnityEngine;

public class Cannon : MonoBehaviour
{
    private Vector2 inputDrag;
    private Vector2 previousMousePosition;

    [SerializeField] private Transform sideMovementRoot;
    [SerializeField] private Transform cannonRightLimit;
    [SerializeField] private Transform cannonLeftLimit;
    private float cannonRightLimitX => cannonRightLimit.localPosition.x;
    private float cannonLeftLimitX => cannonLeftLimit.localPosition.x;

    private float sideMovementSensitivity => SettingsManager.GameSettings.sideMovementSensitivity;

    private float _spawnInterval => SettingsManager.GameSettings.spawnIntervalForPlayer;
    private float spawnInterval;

    [SerializeField] private int numberOfPlayerThrown = 0;
    private int numberOfPlayerThrownForSpawnGiant => SettingsManager.GameSettings.numberOfPlayerThrownForSpawnGiant;

    private void Awake()
    {
        spawnInterval = _spawnInterval;
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

            spawnInterval -= Time.deltaTime;
            if (spawnInterval <= Time.deltaTime)
            {
                var localPos = sideMovementRoot.position;
                var rotation = sideMovementRoot.transform.rotation;
                GameManager.Instance.SpawnRequest("Player", localPos, rotation, 1);

                spawnInterval = _spawnInterval;
                numberOfPlayerThrown++;

                GameManager.Instance.SetCurrentThrownPctOnBar(numberOfPlayerThrown);
            }
        }
        else
        {
            inputDrag = Vector2.zero;
            spawnInterval = _spawnInterval;
            //here is for big yellow one if i can add -->i added :D
            if (numberOfPlayerThrown >= numberOfPlayerThrownForSpawnGiant)
            {
                var localPos = sideMovementRoot.position;
                var rotation = sideMovementRoot.transform.rotation;
                GameManager.Instance.SpawnRequest("Giant", localPos, rotation, 1);

                numberOfPlayerThrown = 0;
                GameManager.Instance.SetCurrentThrownPctOnBar((float) numberOfPlayerThrown);
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