using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Services.LevelPlay;
using UnityEngine;
using UnityEngine.Rendering;

public class RewardedAdManager : MonoBehaviour
{
    [SerializeField] private string rewardAdId = "";

    private LevelPlayRewardedAd rewardedAdUnit;

    private static RewardedAdManager Instance;

    public event Action OnAdStartedBeingShown;

    public event Action OnAdClosed;

    public event Action RewardPlayer;

    private bool isInitialized = false;

    private void Awake()
    {
        DoNotDestroy();
    }

    /*
    private void OnEnable()
    {
        InitializeAdUnit();
        rewardedAdUnit.OnAdLoaded += RewardedOnAdLoadedEvent;
        rewardedAdUnit.OnAdLoadFailed += RewardedOnAdLoadFailedEvent;
        rewardedAdUnit.OnAdDisplayed += RewardedOnAdDisplayedEvent;
        rewardedAdUnit.OnAdDisplayFailed += RewardedOnAdDisplayFailedEvent;
        rewardedAdUnit.OnAdRewarded += RewardedOnAdRewardedEvent;
        rewardedAdUnit.OnAdClosed += RewardedOnAdClosedEvent;
        // Optional 
        rewardedAdUnit.OnAdClicked += RewardedOnAdClickedEvent;
        rewardedAdUnit.OnAdInfoChanged += RewardedOnAdInfoChangedEvent;
        InitializeAdManager.InitializedAds += LoadAd;
    }
    */

    private void Start()
    {
        InitializeAdUnit();
        rewardedAdUnit.OnAdLoaded += RewardedOnAdLoadedEvent;
        rewardedAdUnit.OnAdLoadFailed += RewardedOnAdLoadFailedEvent;
        rewardedAdUnit.OnAdDisplayed += RewardedOnAdDisplayedEvent;
        rewardedAdUnit.OnAdDisplayFailed += RewardedOnAdDisplayFailedEvent;
        rewardedAdUnit.OnAdRewarded += RewardedOnAdRewardedEvent;
        rewardedAdUnit.OnAdClosed += RewardedOnAdClosedEvent;
        // Optional 
        rewardedAdUnit.OnAdClicked += RewardedOnAdClickedEvent;
        rewardedAdUnit.OnAdInfoChanged += RewardedOnAdInfoChangedEvent;
        InitializeAdManager.InitializedAds += LoadAd;
    }

    private void OnDisable()
    {
        //rewardedAdUnit.OnAdLoaded -= RewardedOnAdLoadedEvent;
        //rewardedAdUnit.OnAdLoadFailed -= RewardedOnAdLoadFailedEvent;
        //rewardedAdUnit.OnAdDisplayed -= RewardedOnAdDisplayedEvent;
        //rewardedAdUnit.OnAdDisplayFailed -= RewardedOnAdDisplayFailedEvent;
        //rewardedAdUnit.OnAdRewarded -= RewardedOnAdRewardedEvent;
        //rewardedAdUnit.OnAdClosed -= RewardedOnAdClosedEvent;
        // Optional 
        //rewardedAdUnit.OnAdClicked -= RewardedOnAdClickedEvent;
        //rewardedAdUnit.OnAdInfoChanged -= RewardedOnAdInfoChangedEvent;
        InitializeAdManager.InitializedAds -= LoadAd;
    }

    private void DoNotDestroy()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void InitializeAdUnit()
    {
        if (!isInitialized)
        {
            rewardedAdUnit = new LevelPlayRewardedAd(rewardAdId, null);
            Debug.Log("Initialized Rewarded Ad");
        } else
        {
            Debug.Log("Already Initialised");
        }

    }

    private void LoadAd()
    {
        rewardedAdUnit.LoadAd();

        Debug.Log("Loading Rewarded Ad");
    }

    private void RewardedOnAdLoadedEvent(LevelPlayAdInfo adInfo)
    {
        Debug.Log("Ad Loaded");
    }
    private void RewardedOnAdLoadFailedEvent(LevelPlayAdError error)
    {
        Debug.Log($"Ad Failed to Load with Error {error.ToString()}");

        LoadAd();
    }

    // Activate When ad starts show 
    private void RewardedOnAdDisplayedEvent(LevelPlayAdInfo adInfo)
    {
        Debug.Log("Displayed");
        OnAdStartedBeingShown?.Invoke();
    }
    private void RewardedOnAdDisplayFailedEvent(LevelPlayAdInfo adInfo, LevelPlayAdError error)
    {
        Debug.Log($"Not Displayed cause of Error {error.ToString()}");
    }
    private void RewardedOnAdRewardedEvent(LevelPlayAdInfo adInfo, LevelPlayReward adReward)
    {
        Debug.Log($"Rewarded with {adReward.Name} + {adReward.Amount}");
        RewardForAd();
    }
    private void RewardedOnAdClosedEvent(LevelPlayAdInfo adInfo)
    {
        Debug.Log("OnAdClosed");
        OnAdClosed?.Invoke();
        //isInitialized = false;
        LoadAd();
    }
    private void RewardedOnAdClickedEvent(LevelPlayAdInfo adInfo)
    {
        Debug.Log("OnAdClicked");
    }
    private void RewardedOnAdInfoChangedEvent(LevelPlayAdInfo adInfo)
    {
        Debug.Log("OnAdInfoChanged");
    }

    public bool IsAdReady() { return rewardedAdUnit.IsAdReady(); }

    public void ShowAd()
    {
        if (rewardedAdUnit.IsAdReady())
        {
            rewardedAdUnit.ShowAd();
        }
    }

    private void RewardForAd()
    {
        RewardPlayer?.Invoke();
    }
}
