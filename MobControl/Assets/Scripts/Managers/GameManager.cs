using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    
    public int  DecreaseTowerPoint(int towerPoint)
    {
        return towerPoint - 1;
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
