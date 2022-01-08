using System;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    private int giantHp;
    private int numberOfPlayerThrownForSpawnGiant => SettingsManager.GameSettings.numberOfPlayerThrownForSpawnGiant;
    [SerializeField] private Vector3 destination;
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

    public void SetDestination(Vector3 playerDestination)
    {
        destination = playerDestination;
    }

    public Vector3 GetDestination()
    {
        return destination;
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