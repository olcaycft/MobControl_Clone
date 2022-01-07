using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Giant : MonoSingleton<Giant>
{
    [SerializeField] public int hp = 5;
    private Vector3 firstScale;

    private void Awake()
    {
        firstScale = gameObject.transform.localScale;
    }

    private void ScaleChanger()
    {
        var localScale = gameObject.transform.localScale;
        localScale.x -= 0.2f;
        localScale.y -= 0.2f;
        localScale.z -= 0.2f;
        gameObject.transform.localScale = localScale;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            hp--;
            ScaleChanger();
            if (hp<=0)
            {
                gameObject.SetActive(false);
                gameObject.transform.localScale = firstScale;
            }
        }
        else if (collision.gameObject.CompareTag("Tower"))
        {
            Tower.Instance.SetGiantHP(hp);
        }
    }
}
