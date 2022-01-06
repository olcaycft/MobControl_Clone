using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Game Settings")]
public class GameSettings : ScriptableObject
{
    public float sideMovementSensitivity = 0.5f;
    public float spawnInterval = 0.5f;
    public float playerSpeedChangeTime = 0.5f;
    public float playerSingleStepSpeedAtTowerArea = 1f;
}