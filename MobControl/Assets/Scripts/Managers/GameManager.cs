using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private Vector3 destination;
    public int  DecreaseTowerPoint(int towerPoint)
    {
        return towerPoint - 1;
        
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
}
