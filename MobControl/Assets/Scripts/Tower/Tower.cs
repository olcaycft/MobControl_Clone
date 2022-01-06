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
    }

    private void FixedUpdate()
    {
        TowerPointChecker();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.SetActive(false);
            towerPoint -= 1;
        }
    }

    private void SpawnRoutine()
    {
        for (int i = 0; i < 25; i++)
        {
            float degisimZ = Random.Range(0f, 2.5f);
            float degisimY = Random.Range(-3.5f, +3.5f);
            ObjectPooler.Instance.SpawnFromPool("Enemy",
                new Vector3(transform.position.x+degisimY, transform.position.y+0.3f, transform.position.z - degisimZ),
                new Quaternion(0f,180f,0f,0f));
        }
        
    }

    private void TowerPointChecker()
    {
        if (towerPoint <=0)
        {
            GameManager.Instance.Won();
        }
    }
}