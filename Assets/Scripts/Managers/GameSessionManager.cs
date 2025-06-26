using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSessionManager : MonoBehaviour {
    [SerializeField] private GamePointsManager gamePointsManager;
    [SerializeField] private GamePauseManager gamePauseManager;

    public static event Action<int> PointsIncreased;
    public static event Action<int> SentPointsOnGameEnded;
    public static event Action GamePauseOnHide;
    public static event Action OnGameStarted;
    public static event Action OnGameRun;
    public static event Action OnGamePaused;
    public static event Action OnGameFinished;
    public static event Action OnRevived;


    public GameState CurrentGameState { get; private set; }

    private void Awake()
    {
        if (gamePointsManager == null) gamePointsManager = FindAnyObjectByType<GamePointsManager>();
        if (gamePauseManager == null) gamePauseManager = FindAnyObjectByType<GamePauseManager>();
        Application.targetFrameRate = 60;
    }

    private void Start()
    {
        ChangeGameState(GameState.Start, () =>
        {
            OnGameStarted?.Invoke();
        });
    }

    private void OnEnable()
    {
        Player.OnPickedPoint += IncreasePoints;
        Player.OnPlayerCrushed += EndTheGame;
        UIManager.PauseTheGame += Pause;
        UIManager.ResumeTheGame += Resume;
        UIManager.OpenAdForRevive += ContinueAfterRevive;
        LevelGeneratorManager.OnStartLevelGenerated += RunGame;
    }

    private void OnDisable()
    {
        Player.OnPickedPoint -= IncreasePoints;
        Player.OnPlayerCrushed -= EndTheGame;
        UIManager.PauseTheGame -= Pause;
        UIManager.ResumeTheGame -= Resume;
        UIManager.OpenAdForRevive -= ContinueAfterRevive;
        LevelGeneratorManager.OnStartLevelGenerated -= RunGame;
    }

    private void IncreasePoints(int point)
    {
        gamePointsManager.IncreasePoints(point);
        SetPointsToUI();
    }

    private void RunGame()
    {
        ChangeGameState(GameState.Run, () =>
        {
            OnGameRun?.Invoke();
        });
    }

    public void Pause()
    {
        if (CurrentGameState == GameState.Run)
        {
            ChangeGameState(GameState.Pause, () =>
            {
                gamePauseManager.Pause();
                OnGamePaused?.Invoke();
            });
        }

    }

    public void Resume()
    {
        if (CurrentGameState == GameState.Pause)
        {
            ChangeGameState(GameState.Run, () =>
            {
                gamePauseManager.Resume();
                OnGameRun?.Invoke();
            });
        }
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            Debug.Log("OnHidePause");
            if (CurrentGameState == GameState.Run)
            {
                GamePauseOnHide?.Invoke();
                Pause();
            }
            else
            {
                gamePauseManager.Pause();
            }
        }
        else
        {
            Debug.Log("OnHideResume");
            if (CurrentGameState == GameState.Pause)
            {
                Resume();
            }
            else
            {
                gamePauseManager.Resume();
            }
        }
    }

    private void SetPointsToUI()
    {
        PointsIncreased?.Invoke(gamePointsManager.GetPoints());
    }

    private void ContinueAfterRevive()
    {
        if (CurrentGameState == GameState.Finish) {
            ChangeGameState(GameState.Run, () =>
            {
                //gamePauseManager.Resume();
                OnGameRun?.Invoke();
                OnRevived?.Invoke();
            });
        }
    }

    private void EndTheGame()
    {
        ChangeGameState(GameState.Finish, () =>
        {
            OnGameFinished?.Invoke();
            SentPointsOnGameEnded?.Invoke(gamePointsManager.GetPoints());
        });

    }

    private void ChangeGameState(GameState state, Action OnStateChanged)
    {
        CurrentGameState = state;
        OnStateChanged?.Invoke();
    }
}

[Serializable]
public enum GameState {
    Start,
    Run,
    Pause,
    Finish
}

