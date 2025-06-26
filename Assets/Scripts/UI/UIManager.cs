using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameUIManager gameUIManager;
    [SerializeField] private PauseUIManager pauseUIManager;
    [SerializeField] private FinalUIManager finalUIManager;
    [SerializeField] private UIBuffItemsHolder itemsHolder;

    public static event Action PauseTheGame;
    public static event Action ResumeTheGame;
    public static event Action GoToMainMenu;
    public static event Action OpenAdForRevive;

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
        GameUIManager.PauseGame += Pause;
        GameSessionManager.PointsIncreased += PointsToUI;
        GameSessionManager.GamePauseOnHide += Pause;
        GameSessionManager.SentPointsOnGameEnded += SetFinalUI;
        GameSessionManager.OnRevived += ClosesFinalUIOnRevive;
        Player.OnBuffApplied += itemsHolder.ActivateBuffUI;
    }


    private void OnDisable()
    {
        PauseUIManager.Resume -= Resume;
        PauseUIManager.GoToMenuFromPause -= GoToMenuFromPause;
        FinalUIManager.GoToMenu -= GoToMenu;
        FinalUIManager.ReviveOnAd -= GoToAd;
        GameUIManager.PauseGame -= Pause;
        GameSessionManager.PointsIncreased -= PointsToUI;
        GameSessionManager.GamePauseOnHide -= Pause;
        GameSessionManager.SentPointsOnGameEnded -= SetFinalUI;
        GameSessionManager.OnRevived -= ClosesFinalUIOnRevive;
        Player.OnBuffApplied -= itemsHolder.ActivateBuffUI;
    }

    private void SetNullValues()
    {
        if (gameUIManager == null) gameUIManager = GetComponentInChildren<GameUIManager>();
        if (pauseUIManager == null) pauseUIManager = GetComponentInChildren<PauseUIManager>();
        if (finalUIManager == null) finalUIManager = GetComponentInChildren<FinalUIManager>();
    }

    private void SetBaseUI()
    {
        gameUIManager.OpenUI();
        pauseUIManager.CloseUI();
        finalUIManager.CloseUI();

        gameUIManager.OpenUI(() =>
        {
            Debug.Log("asdsad");
        });
    }

    private void Update()
    {
        itemsHolder.UpdateBuffTimer();
    }

    private void Resume()
    {
        
        pauseUIManager.CloseUI(() =>
        {
            ResumeTheGame?.Invoke();
        });
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
            ResumeTheGame?.Invoke();
            GoToMainMenu?.Invoke();
        });
    }

    private void PointsToUI(int points)
    {
        gameUIManager.SetCurrentPoinsToUIText(points);
    }

    private void SetFinalUI(int points)
    {
        finalUIManager.SetPointsToUIText(points);
        finalUIManager.OpenUI();
    }

    private void ClosesFinalUIOnRevive()
    {
        finalUIManager.CloseUI();
    }
}
