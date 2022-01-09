using System;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    private int giantHp;
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
        else if (tag.Equals("Giant"))
        {
            towerPoint -= giantHp;
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

    public void SetCannonDestination(Vector3 playerDestination)
    {
        cannonDestination = playerDestination;
    }

    public Vector3 GetCannonDestination()
    {
        return cannonDestination;
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

    public void GiantHitTower(int giantHp)
    {
        this.giantHp = giantHp;
    }
}