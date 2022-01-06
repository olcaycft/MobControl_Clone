using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Tower : MonoBehaviour
{
    private float spawnInterval => SettingsManager.GameSettings.spawnIntervalForEnemy;
    private void Awake()
    {
        InvokeRepeating(nameof(SpawnRoutine),spawnInterval,spawnInterval);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.SetActive(false);
            GameManager.Instance.DecreaseTowerPoint();
        }
    }

    private void SpawnRoutine()
    {
        for (int i = 0; i < 5; i++)
        {
            float degisimZ = Random.Range(0f, 1f);
            float degisimY = Random.Range(-2f, +2f);
            ObjectPooler.Instance.SpawnFromPool("Enemy",
                new Vector3(transform.position.x+degisimY, transform.position.y+0.3f, transform.position.z - degisimZ),
                new Quaternion(0f,180f,0f,0f));
        }
        
    }
}