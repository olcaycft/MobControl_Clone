using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerMovement : MonoBehaviour,IPooledObject
{
    private float speed = 12f;
    private float playerSpeedChangeTime => SettingsManager.GameSettings.playerSpeedChangeTime;
    private bool isPlayerCome = false;
    
    private void Update()
    {
        if (isPlayerCome)
        {
            OnObjectSpawn();
            isPlayerCome = false;
        }
        else
        {
            OnObjectSpawn();
        }
        
    }

    public void OnObjectSpawn()
    {
        transform.position += transform.forward*Time.deltaTime*speed;
    }
    private void playerSpeedChanger()
    {
        speed = 6f;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerRusher"))
        {
            isPlayerCome = true;
            speed = 12f;
            Invoke(nameof(playerSpeedChanger),playerSpeedChangeTime);
        }
    }
}