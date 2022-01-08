using UnityEngine;

public class Giant : MonoBehaviour
{
    [SerializeField] private int _giantHp => SettingsManager.GameSettings.giantHp;
    private int giantHp;
    private Vector3 firstScale;

    private void Awake()
    {
        giantHp = _giantHp;
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
            giantHp--;
            ScaleChanger();
            if (giantHp <= 0)
            {
                gameObject.SetActive(false);
                giantHp = _giantHp;
                gameObject.transform.localScale = firstScale;
            }
        }
        else if (collision.gameObject.CompareTag("Tower"))
        {
            //i hit tower giantHp its my current hp.
            GameManager.Instance.GiantHitTower(giantHp);
            giantHp = _giantHp;
        }
    }
}