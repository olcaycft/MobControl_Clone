using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private int towerPoint = 50;
    public void DecreaseTowerPoint()
    {
        towerPoint-=1;
    }
}
