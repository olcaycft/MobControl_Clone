using UnityEngine;
using Random = UnityEngine.Random;

public class CollisionGate : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("2xGate"))
        {
            gameObject.SetActive(false);
            GameManager.Instance.SpawnRequest(gameObject.tag, transform.position, transform.rotation, 2);
        }
        else if (other.gameObject.CompareTag("3xGate"))
        {
            gameObject.SetActive(false);
            GameManager.Instance.SpawnRequest(gameObject.tag, transform.position, transform.rotation, 3);
        }
        else if (other.gameObject.CompareTag("4xGate"))
        {
            gameObject.SetActive(false);
            GameManager.Instance.SpawnRequest(gameObject.tag, transform.position, transform.rotation, 4);
        }
    }
}