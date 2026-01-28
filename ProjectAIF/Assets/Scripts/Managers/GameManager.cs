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
    [SerializeField] private int _maxPlayerLevel = 100;
    public int MaxPlayerLevel => _maxPlayerLevel;
    // ToDo: Crystal Data

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
