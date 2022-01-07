using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Tower : MonoBehaviour
{
    [SerializeField] private int towerPoint = 50;
    private float spawnInterval => SettingsManager.GameSettings.spawnIntervalForEnemy;
    private void Awake()
    {
        InvokeRepeating(nameof(SpawnRoutine),spawnInterval,spawnInterval);
        GameManager.Instance.SetDestination(transform.position);
        TowerPointChanger.Instance.ChangeTowerPoint(towerPoint);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.SetActive(false);
            towerPoint -= 1;
            
            TowerPointChanger.Instance.ChangeTowerPoint(towerPoint);
            
            if (towerPoint<=0)
            {
                TowerPointChecker();
            }
            
        }
    }

    private void SpawnRoutine()
    {
        for (int i = 0; i < 25; i++)
        {
            float arrangeX = Random.Range(-3.5f, +3.5f);
            float arrangeZ = Random.Range(0f, 2.5f);
            ObjectPooler.Instance.SpawnFromPool("Enemy",
                new Vector3(transform.position.x+arrangeX, transform.position.y+0.3f, transform.position.z - arrangeZ),
                new Quaternion(0f,180f,0f,0f));
        }
        
    }

    private void TowerPointChecker()
    {
            GameManager.Instance.Won();
    }
}