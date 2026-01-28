using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : SingleTon<GameManager>
{
    // Game Data
    public bool IsGameOver { get; private set; }
    public UnityEvent OnGameOver;
    public int StageNumber { get; private set; }
    
    // Game Objects Data
    // ToDo: Player Data
    // ToDo: Crystal Data
    
    // 아래 값을 이용하여 Enemy 가
    // Player 를 쫒아 갈 것/공격 할 것 인지
    // 혹은 Crystal 을 쫒아 갈 것/공격할 것 인지
    // 결정하게 됨.
    public bool IsCrystalNearPlayer = true;

    private void Awake()
    {
        SingleTonInit();
    }
    
    public void GameOver()
    {
        IsGameOver = true;
        OnGameOver?.Invoke();
    }

    public void GoToNextStage()
    {
        StageNumber++;
    }
}
