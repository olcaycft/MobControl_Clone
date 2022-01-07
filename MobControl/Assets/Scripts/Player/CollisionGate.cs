using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CollisionGate : MonoBehaviour
{
    private float arrangeX;
    private float zChange = 0.5f;
    private float minRange;
    private float maxRange;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("2xGate"))
        {
            SetActiveAfterGate(2);
        }
        else if (other.gameObject.CompareTag("3xGate"))
        {
            SetActiveAfterGate(3);
        }
        else if (other.gameObject.CompareTag("4xGate"))
        {
            SetActiveAfterGate(4);
        }
    }


    private void SetActiveAfterGate(int count)
    {
        gameObject.SetActive(false);
        if (count==2)
        {
            minRange = -0.8f;
            maxRange = 0.8f;
        }
        else if (count==3)
        {
            minRange = -1f;
            maxRange = 1f;
        }
        else if (count==4)
        {
            minRange = -1.3f;
            maxRange = 1.3f;
        }

        for (int i = 0; i < count; i++)
        {
            zChange += 0.1f;
            arrangeX = Random.Range(minRange, maxRange);
            var pos = transform.position;
            pos.x += arrangeX;
            pos.z += zChange;
            ObjectPooler.Instance.SpawnFromPool("Player", pos, transform.rotation);
        }

        zChange = 0.1f;
    }
}