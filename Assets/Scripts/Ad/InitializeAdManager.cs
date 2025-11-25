using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.Services.LevelPlay;
using UnityEngine;

public class InitializeAdManager : MonoBehaviour
{
    [SerializeField] private string appKey = "";

    private bool hasInitilized = false;

    public static event Action InitializedAds;

    private void OnEnable()
    {
        LevelPlay.OnInitSuccess += SdkInitialized;
        LevelPlay.OnInitFailed += SdkNotInitialized;
        AuthenticationManager.OnPlayerSignIn += InitializeAds;
    }

    private void OnDisable()
    {
        LevelPlay.OnInitSuccess -= SdkInitialized;
        LevelPlay.OnInitFailed -= SdkNotInitialized;
        AuthenticationManager.OnPlayerSignIn -= InitializeAds;
    }


    private void SdkInitialized(LevelPlayConfiguration configuration)
    {
        print("Sdk is initialized!!");
        hasInitilized = true;
        InitializedAds?.Invoke();
    }

    private void SdkNotInitialized(LevelPlayInitError initError)
    {
        Debug.Log("Sdk is not initialized on Enable!!");
        Debug.Log(initError.ToString());
        InitializeAds();
    }

    public void InitializeAds()
    {
        if (!hasInitilized)
        {
            LevelPlay.Init(appKey, null);

            Debug.Log("InitializingAds");
        }
    }
}
