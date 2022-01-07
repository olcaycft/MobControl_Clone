using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CollisionGate : MonoBehaviour
{
    private float arrangeX;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("2xGate"))
        {
            gameObject.SetActive(false);
            for (int i = 0; i < 2; i++)
            {
                arrangeX = Random.Range(-0.8f, +0.8f);
                var pos = transform.position;
                pos.x += arrangeX;
                pos.z += 0.5f;
                ObjectPooler.Instance.SpawnFromPool("Player", pos, transform.rotation);
            }
        }
    }
}