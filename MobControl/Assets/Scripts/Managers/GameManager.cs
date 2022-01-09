using System;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    private int playerGiantHpForTower;
    private int playerGiantHpForEnemyGiant;
    private int enemyGiantHp;
    private int numberOfPlayerThrownForSpawnGiant => SettingsManager.GameSettings.numberOfPlayerThrownForSpawnGiant;
    [SerializeField] private Vector3 towerDestination;
    [SerializeField] private Vector3 cannonDestination;
    public event Action<float> OnProgressChange;

    public int DecreaseTowerPoint(string tag, int hit, int towerPoint)
    {
        if (tag.Equals("Player"))
        {
            towerPoint -= hit;
        }
        else if (tag.Equals("GiantPlayer"))
        {
            towerPoint -= playerGiantHpForTower;
        }

        return towerPoint;
    }

    public void SetTowerDestination(Vector3 playerDestination)
    {
        towerDestination = playerDestination;
    }

    public Vector3 GetTowerDestination()
    {
        return towerDestination;
    }

    public void GameOver()
    {
        Debug.Log("GAME OVER");
    }

    public void Won()
    {
        Debug.Log("You Win");
    }

    public void SpawnRequest(string tag, Vector3 pos, Quaternion rot, int count)
    {
        SpawnManager.Instance.SpawnPeople(tag, pos, rot, count);
    }

    public void SetCurrentThrownPctOnBar(float numberOfPlayerThrown)
    {
        float currentThrownPct = numberOfPlayerThrown / numberOfPlayerThrownForSpawnGiant;
        OnProgressChange?.Invoke(currentThrownPct);
    }

    public void PlayerGiantHitTower(int giantHp)
    {
        this.playerGiantHpForTower = giantHp;
    }

    public void SetPlayerGiantHitEnemyGiant(int giantHp)
    {
        this.playerGiantHpForEnemyGiant = giantHp;
    }

    public int GetPlayerGiantHp()
    {
        return this.playerGiantHpForEnemyGiant;
    }

    public void SetEnemyGiantHitPlayerGiant(int giantHp)
    {
        this.enemyGiantHp = giantHp;
    }

    public int GetEnemyGiantHp()
    {
        return this.enemyGiantHp;
    }
}