using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSessionManager : MonoBehaviour {
    [SerializeField] private GamePointsManager gamePointsManager;
    [SerializeField] private GamePauseManager gamePauseManager;
    [SerializeField] private PlayerSpawnManager playerSpawnManager;
    [SerializeField] private CharacterTransporter characterTransporter;
    [SerializeField] private DefaultCharacterHandler defaultCharacterHandler;
    [SerializeField] private FollowingObject playerFollowingObject;
    [SerializeField] private LevelLoader levelLoader;

    [SerializeField] private SaveSystemManager saveSystemManager;

    public static event Action<int> PointsIncreased;
    public static event Action<int> SentPointsOnGameEnded;
    public static event Action GamePauseOnHide;
    public static event Action OnGameStarted;
    public static event Action OnGameRun;
    public static event Action OnGamePaused;
    public static event Action OnGameFinished;
    public static event Action OnRevived;


    public GameState CurrentGameState { get; private set; }

    //private CharacterSO character;

    private void Awake()
    {
        if (gamePointsManager == null) gamePointsManager = FindAnyObjectByType<GamePointsManager>();
        if (defaultCharacterHandler == null) defaultCharacterHandler = FindAnyObjectByType<DefaultCharacterHandler>();
        if (gamePauseManager == null) gamePauseManager = FindAnyObjectByType<GamePauseManager>();
        if(playerSpawnManager == null) playerSpawnManager = FindAnyObjectByType<PlayerSpawnManager>();
        if (characterTransporter == null) characterTransporter = FindAnyObjectByType<CharacterTransporter>();
        if(levelLoader == null) levelLoader = FindAnyObjectByType<LevelLoader>();
        if(saveSystemManager == null) saveSystemManager = FindAnyObjectByType<SaveSystemManager>();
        Application.targetFrameRate = 60;
    }

    private void Start()
    {
        SpawnPlayerOnStart(GetCharacterToSpawn());

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
        UIManager.GoToMainMenu += GoToMenu;
        LevelGeneratorManager.OnStartLevelGenerated += RunGame;
        //LevelLoader.OnLevelLoaded += SpawnPlayerOnAwake;
        //LevelLoader.OnLevelLoaded += Test;
    }

    private void OnDisable()
    {
        Player.OnPickedPoint -= IncreasePoints;
        Player.OnPlayerCrushed -= EndTheGame;
        UIManager.PauseTheGame -= Pause;
        UIManager.ResumeTheGame -= Resume;
        UIManager.OpenAdForRevive -= ContinueAfterRevive;
        UIManager.GoToMainMenu -= GoToMenu;
        LevelGeneratorManager.OnStartLevelGenerated -= RunGame;
        //LevelLoader.OnLevelLoaded -= SpawnPlayerOnAwake;
    }

    private CharacterSO GetCharacterToSpawn()
    {
        if(characterTransporter ==  null)
        {
            Debug.Log("Default");
            return defaultCharacterHandler.GetDefaultCharacter();
        } else
        {
            Debug.Log("Transported");
            return characterTransporter.GetTransportedCharacter();
        }
    }

    private void SpawnPlayerOnStart(CharacterSO characterSOToSpawn)
    {
        GameObject characterToSpawn = characterSOToSpawn.characterForGame;

        GameObject spawnedPlayer = playerSpawnManager.SpawnMenuPlayer(characterToSpawn);

        playerFollowingObject.SetTargetToFollow(spawnedPlayer);

        Debug.Log("Spawned");
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
                Debug.Log($"Paused {pause} + State {CurrentGameState}");
            }
            else
            {
                gamePauseManager.Pause();
                Debug.Log($"Paused 1 {pause} + State {CurrentGameState}");
            }
        }
        else
        {
            Debug.Log("OnHideResume");

            if(CurrentGameState == GameState.Finish)
            {
                gamePauseManager.Resume();
                Debug.Log($"UnPaused 1 {pause} + State {CurrentGameState}");
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

    private void GoToMenu()
    {
        if(CurrentGameState == GameState.Finish)
        {
            gamePointsManager.SavePoints();
            //SceneManager.LoadScene(0);
            levelLoader.LoadMainMenu();
            Debug.Log($"Go to menu {CurrentGameState}");
        }
    }
}

[Serializable]
public enum GameState {
    Start,
    Run,
    Pause,
    Finish
}

