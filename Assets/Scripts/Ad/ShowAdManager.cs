using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowAdManager : MonoBehaviour
{
    [SerializeField] private RewardedAdManager rewardedAdManager;

    public event Action PauseOnAdStarted;
    public event Action UnPauseOnAdClosed;

    public event Action RevivePlayerOnReward;

    private void Awake()
    {
        rewardedAdManager = FindAnyObjectByType<RewardedAdManager>();
    }

    private void OnEnable()
    {
        rewardedAdManager.OnAdStartedBeingShown += OnAdStartedBeingShown;
        rewardedAdManager.OnAdClosed += OnAdClosed;
        rewardedAdManager.RewardPlayer += RewardForAd;
    }

    private void OnDisable()
    {
        rewardedAdManager.OnAdStartedBeingShown -= OnAdStartedBeingShown;
        rewardedAdManager.OnAdClosed -= OnAdClosed;
        rewardedAdManager.RewardPlayer -= RewardForAd;
    }


    public void ShowAd()
    {
        rewardedAdManager.ShowAd();
    }

    public bool IsAdReadyToBeShown() { return rewardedAdManager.IsAdReady(); }

    private void OnAdStartedBeingShown()
    {
        PauseOnAdStarted?.Invoke();
    }


    private void OnAdClosed()
    {
        UnPauseOnAdClosed?.Invoke();
    }

    private void RewardForAd()
    {
        RevivePlayerOnReward?.Invoke();
    }
}
