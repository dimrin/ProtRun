using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;

public class AuthenticationManager : MonoBehaviour
{

    public static event Action OnPlayerSignIn;

    [SerializeField] private GooglePlayAuthenticator googlePlayGamesAuthenticator;

    public static event Action TurnOfLoginGoogleButton;

    private void Awake()
    {
        if(googlePlayGamesAuthenticator == null) googlePlayGamesAuthenticator = FindAnyObjectByType<GooglePlayAuthenticator>();
    }

    private void OnEnable()
    {
        UnityServices.Initialized += StartAnonymousSignIn;
        MenuUIManager.OnLoginGoogleButtonClicked += LoginWithGooglePlayGames;
        //MenuUIManager.OnLoginGoogleButtonClicked += SignInWIthGooglePlayGames;
    }

    private void OnDisable()
    {
        MenuUIManager.OnLoginGoogleButtonClicked -= LoginWithGooglePlayGames;
        //MenuUIManager.OnLoginGoogleButtonClicked -= SignInWIthGooglePlayGames;
    }

    // initialize in GameEnter 
    public async Task InitializeUnityServices()
    {
        if (UnityServices.State == ServicesInitializationState.Uninitialized)
        {
            Debug.Log("Services Initializing");
            await UnityServices.InitializeAsync();
        }
    }
    private async void StartAnonymousSignIn()
    {
        Debug.Log("StartAnonymousSignIn");
        await SignInAnonymouslyAsync();
    }

    private async Task SignInAnonymouslyAsync()
    {
        try
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
            Debug.Log("Sign in anonymously succeeded!");
            OnPlayerSignIn?.Invoke();
            //googlePlayGamesAuthenticator.ActivateGooglePlayGames();
            /*
            googlePlayGamesAuthenticator.TryToSignInViaGooglePlayGames(()=>
            {
                TurnOfLoginGoogleButton?.Invoke();
            });
            */

            // Shows how to get the playerID
            Debug.Log($"PlayerID: {AuthenticationService.Instance.PlayerId}");

        }
        catch (AuthenticationException ex)
        {
            // Compare error code to AuthenticationErrorCodes
            // Notify the player with the proper error message
            Debug.LogException(ex);
        }
        catch (RequestFailedException ex)
        {
            // Compare error code to CommonErrorCodes
            // Notify the player with the proper error message
            Debug.LogException(ex);
        }
    }

    private void LoginWithGooglePlayGames()
    {
        googlePlayGamesAuthenticator.ActivateGooglePlayGames();
        googlePlayGamesAuthenticator.LoginViaGooglePlayGames(() =>
        {
            TurnOfLoginGoogleButton?.Invoke();
        });
    }

    private void SignInWIthGooglePlayGames()
    {
        googlePlayGamesAuthenticator.TryToSignInViaGooglePlayGames(() =>
        {
            //TurnOfLoginGoogleButton?.Invoke();
        });
    }
}
