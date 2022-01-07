using UnityEditor.AssetImporters;
using UnityEngine;
using Random = UnityEngine.Random;

public class Tower : MonoSingleton<Tower>
{
    private int _towerPoint =>SettingsManager.GameSettings.towerPointSetting;
    [SerializeField] private int towerPoint;
    private float spawnInterval => SettingsManager.GameSettings.spawnIntervalForEnemy;
    private int enemyCount => SettingsManager.GameSettings.enemyCount;
    private int giantHp;
    private void Awake()
    {
        towerPoint = _towerPoint;
        InvokeRepeating(nameof(EnemySpawnRoutine),spawnInterval,spawnInterval);
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
                towerPoint -= 1;
            }
            else if(collisionName.Equals("Giant"))
            {
                var giantHp = GetGiantHP();
                towerPoint -= giantHp ;
            }
            
            TowerPointChanger.Instance.ChangeTowerPoint(towerPoint);
            
            if (towerPoint<=0)
            {
                TowerPointChecker();
            }
        }
    }

    private void EnemySpawnRoutine()
    {
        GameManager.Instance.SpawnRequest("Enemy",transform.position, new Quaternion(0f,180f,0f,0f),enemyCount);
    }

    private void TowerPointChecker()
    { 
        GameManager.Instance.Won();
    }

    public void SetGiantHP(int giantHp)
    {
        this.giantHp = giantHp;
    }
    private int GetGiantHP()
    {
        return this.giantHp;
    }
}