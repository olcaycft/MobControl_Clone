using System;
using UnityEngine;

public class DestinationTracker : MonoBehaviour
{
    [SerializeField] private GameObject destination;
    [SerializeField] private bool isPlayerAtTowerArea = false;
    [SerializeField] private float speed => SettingsManager.GameSettings.playerSingleStepSpeedAtTowerArea;

    private void Awake()
    {
        destination =
            GameObject.Find(
                "Tower"); //i cant or i dont know how to add tower to my player becasue of all of the my players are prefab thats why im using this here.
    }

    private void Update()
    {
        if (isPlayerAtTowerArea)
        {
            GoForDestination();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("TowerArea"))
        {
            Debug.Log("im tower area");
            isPlayerAtTowerArea = true;
        }
        else
        {
            isPlayerAtTowerArea = false;
        }
    }

    private void GoForDestination()
    {
        Vector3 destinationDirection = destination.transform.position - transform.position;
        float singleStep = speed * Time.deltaTime;
        Vector3 newDirection=Vector3.RotateTowards(transform.forward,destinationDirection,singleStep,0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }
}