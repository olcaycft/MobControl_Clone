using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float speed => SettingsManager.GameSettings.enemySpeed;
    private void Update()
    {
        GoForward();
    }

    private void GoForward()
    {
        transform.position += transform.forward * Time.deltaTime * speed;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            collision.gameObject.SetActive(false);
        }
        else if (collision.gameObject.CompareTag("Giant"))
        {
            gameObject.SetActive(false);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerRusher"))
        {
            GameManager.Instance.GameOver();
        }
    }
}
