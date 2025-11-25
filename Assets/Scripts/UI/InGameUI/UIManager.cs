using System;
using UnityEngine;

public class UIManager : MonoBehaviour {
    [SerializeField] private GameUIManager gameUIManager;
    [SerializeField] private PauseUIManager pauseUIManager;
    [SerializeField] private FinalUIManager finalUIManager;
    [SerializeField] private UIBuffItemsHolder itemsHolder;
    [SerializeField] private LoadingUIManager loadingUIManager;

    public static event Action PauseTheGame;
    public static event Action ResumeTheGame;
    public static event Action GoToMainMenu;
    public static event Action GoToMainMenuOnPause;
    public static event Action OpenAdForRevive;
    public static event Action OnLoadingLevelMenuAnimationFinished;
    public static event Action OnStartCountDownEnded;
    public static event Action CheckRevivableState;

    private void Awake()
    {
        SetNullValues();

        SetBaseUI();
    }

    private void OnEnable()
    {
        PauseUIManager.Resume += Resume;
        PauseUIManager.GoToMenuFromPause += GoToMenuFromPause;
        FinalUIManager.GoToMenu += GoToMenu;
        FinalUIManager.ReviveOnAd += GoToAd;
        FinalUIManager.CloseReviveWindow += CloseAdReviveUI;
        GameUIManager.PauseGame += Pause;
        GameUIManager.OnCountDownFinished += StartGame;
        GameUIManager.OnCountDownFinished += ResumeOnAnimationEvent;
        GameSessionManager.PointsIncreased += PointsToUI;
        GameSessionManager.GamePauseOnHide += Pause;
        GameSessionManager.OnGameLoaded += ActivateCountDownUI;
        //GameSessionManager.GameResumeOnWakeUp += Resume;
        loadingUIManager.OnAnimationFinished += OnLevelAnimationFinished;
        GameSessionManager.OnGameFinished += CheckToOpenReviveUI;
        GameSessionManager.SentPointsOnGameEnded += SetFinalUI;
        GameSessionManager.OnRevived += ClosesFinalUIOnRevive;
        GameSessionManager.CheckIfRevivable += TryToOpenReviveUI;
        Player.OnBuffApplied += ActivateBufUI;
    }


    private void OnDisable()
    {
        PauseUIManager.Resume -= Resume;
        PauseUIManager.GoToMenuFromPause -= GoToMenuFromPause;
        FinalUIManager.GoToMenu -= GoToMenu;
        FinalUIManager.ReviveOnAd -= GoToAd;
        FinalUIManager.CloseReviveWindow -= CloseAdReviveUI;
        GameUIManager.PauseGame -= Pause;
        GameUIManager.OnCountDownFinished -= StartGame;
        GameUIManager.OnCountDownFinished -= ResumeOnAnimationEvent;
        GameSessionManager.PointsIncreased -= PointsToUI;
        GameSessionManager.GamePauseOnHide -= Pause;
        GameSessionManager.OnGameLoaded -= ActivateCountDownUI;
        loadingUIManager.OnAnimationFinished -= OnLevelAnimationFinished;
        //GameSessionManager.GameResumeOnWakeUp -= Resume;
        GameSessionManager.OnGameFinished -= CheckToOpenReviveUI;
        GameSessionManager.SentPointsOnGameEnded -= SetFinalUI;
        GameSessionManager.OnRevived -= ClosesFinalUIOnRevive;
        GameSessionManager.CheckIfRevivable -= TryToOpenReviveUI;
        Player.OnBuffApplied -= ActivateBufUI;
    }

    private void SetNullValues()
    {
        if (gameUIManager == null) gameUIManager = GetComponentInChildren<GameUIManager>();
        if (pauseUIManager == null) pauseUIManager = GetComponentInChildren<PauseUIManager>();
        if (finalUIManager == null) finalUIManager = GetComponentInChildren<FinalUIManager>();
        if (loadingUIManager == null) loadingUIManager = GetComponentInChildren<LoadingUIManager>();
    }

    private void SetBaseUI()
    {
        loadingUIManager.ActivateCurrentLevelLoadAnimation();
        gameUIManager.OpenUI();
        pauseUIManager.CloseUI();
        finalUIManager.CloseUI();
    }

    private void Update()
    {
        itemsHolder.UpdateBuffTimer();
    }

    private void ActivateCountDownUI()
    {
        gameUIManager.StartCountDownAnimation();
    }

    private void StartGame()
    {
        OnStartCountDownEnded?.Invoke();
    }

    private void Resume()
    {
        pauseUIManager.CloseUI(() =>
        {
            ActivateCountDownUI();
            //ResumeTheGame?.Invoke();
        });
    }

    private void ResumeOnAnimationEvent()
    {
        ResumeTheGame?.Invoke();
    }

    private void Pause()
    {
        pauseUIManager.OpenUI(() =>
        {
            PauseTheGame?.Invoke();
        });
    }

    private void GoToMenu()
    {
        finalUIManager.CloseUI(() =>
        {
            loadingUIManager.ActivateNewLevelLoadAnimation();
            GoToMainMenu?.Invoke();
        });
    }

    private void GoToAd()
    {
        OpenAdForRevive?.Invoke();
    }

    private void GoToMenuFromPause()
    {
        pauseUIManager.CloseUI(() =>
        {
            //ResumeTheGame?.Invoke();
            //GoToMainMenu?.Invoke();
            GoToMainMenuOnPause?.Invoke();
            loadingUIManager.ActivateNewLevelLoadAnimation();
        });
    }

    private void ActivateBufUI(ItemType itemType, int itemValue)
    {
        itemsHolder.ActivateBuffUI(itemType, itemValue);
    }

    private void OnLevelAnimationFinished()
    {
        OnLoadingLevelMenuAnimationFinished?.Invoke();
    }

    private void PointsToUI(int points)
    {
        gameUIManager.SetCurrentPoinsToUIText(points);
    }

    private void CheckToOpenReviveUI()
    {
        CheckRevivableState?.Invoke();
    }

    private void TryToOpenReviveUI(bool state)
    {
        if(state)
        {
            OpenAdReviveUI();
        }
        else
        {
            OpenFinalUI();
        }
    }

    private void OpenAdReviveUI()
    {
        OpenFinalUI();
        finalUIManager.OpenAdReviveWindow();
    }

    private void OpenFinalUI()
    {
        finalUIManager.OpenUI();
    }

    private void CloseAdReviveUI()
    {
        finalUIManager.CloseAdReviveWindow();
    }

    private void SetFinalUI(int points)
    {
        finalUIManager.SetPointsToUIText(points);
        finalUIManager.OpenFinalWindow();
    }

    private void ClosesFinalUIOnRevive()
    {
        finalUIManager.CloseAdReviveWindow();
        finalUIManager.CloseUI();
    }
}
