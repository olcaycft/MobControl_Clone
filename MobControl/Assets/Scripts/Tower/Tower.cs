using UnityEngine;

public class Tower : MonoBehaviour
{
    private int _towerPoint => SettingsManager.GameSettings.towerPointSetting;
    [SerializeField] private int towerPoint;
    private float spawnInterval => SettingsManager.GameSettings.spawnIntervalForEnemy;
    private int enemyCount => SettingsManager.GameSettings.enemyCount;

    private void Awake()
    {
        towerPoint = _towerPoint;
        InvokeRepeating(nameof(EnemySpawnRoutine), spawnInterval, spawnInterval);
        GameManager.Instance.SetDestination(transform.position);
        TowerPointChanger.Instance.ChangeTowerPoint(towerPoint);
    }

    private void OnCollisionEnter(Collision collision)
    {
        var collisionName = collision.gameObject.tag;
        if (collisionName.Equals("Player") || collisionName.Equals("Giant"))
        {
            collision.gameObject.SetActive(false);

            if (collisionName.Equals("Player"))
            {
                towerPoint=GameManager.Instance.DecreaseTowerPoint("Player",1, towerPoint);
            }
            else if (collisionName.Equals("Giant"))
            {
                towerPoint = GameManager.Instance.DecreaseTowerPoint("Giant",1, towerPoint);
            }

            TowerPointChanger.Instance.ChangeTowerPoint(towerPoint);

            if (towerPoint <= 0)
            {
                TowerDestroy();
            }
        }
    }

    private void EnemySpawnRoutine()
    {
        GameManager.Instance.SpawnRequest("Enemy", transform.position, new Quaternion(0f, 180f, 0f, 0f), enemyCount);
    }

    private void TowerDestroy()
    {
        GameManager.Instance.Won();
    }
}