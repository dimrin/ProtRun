using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSessionManager : MonoBehaviour
{
    [SerializeField] private GamePointsManager gamePointsManager;
    [SerializeField] private GamePauseManager gamePauseManager;

    public static event Action<int> PointsIncreased;
    public static event Action<int> OnGameEnded;
    public static event Action GamePauseOnHide;

    private void Awake()
    {
        if(gamePointsManager == null) gamePointsManager = FindAnyObjectByType<GamePointsManager>();
        if(gamePauseManager == null) gamePauseManager = FindAnyObjectByType<GamePauseManager>();
    }


    private void OnEnable()
    {
        Player.OnPickedPoint += IncreasePoints;
        Player.OnPlayerCrushed += EndTheGame;
        UIManager.PauseTheGame += Pause;
        UIManager.ResumeTheGame += Resume;
    }

    private void OnDisable()
    {
        Player.OnPickedPoint -= IncreasePoints;
        Player.OnPlayerCrushed -= EndTheGame;
        UIManager.PauseTheGame -= Pause;
        UIManager.ResumeTheGame -= Resume;
    }

    private void IncreasePoints(int point)
    {
        gamePointsManager.IncreasePoints(point);
        SetPointsToUI();
    }

    public void Pause()
    {
        gamePauseManager.Pause();
    }

    public void Resume()
    {
        gamePauseManager.Resume();
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause) GamePauseOnHide?.Invoke();
    }

    private void SetPointsToUI()
    {
        PointsIncreased?.Invoke(gamePointsManager.GetPoints());
    }


    private void EndTheGame()
    {
        OnGameEnded?.Invoke(gamePointsManager.GetPoints());
    }
}
