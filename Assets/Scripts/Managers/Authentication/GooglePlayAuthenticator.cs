using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using Unity.Services.Authentication;
using System.Threading.Tasks;
using System;

public class GooglePlayAuthenticator : MonoBehaviour
{
    private string m_GooglePlayGamesAuthToken;


    private void Awake()
    {
        PlayGamesPlatform.DebugLogEnabled = true;

    }


    public void ActivateGooglePlayGames()
    {
        PlayGamesPlatform.Activate();
    }

    public void LoginViaGooglePlayGames(Action OnSignIn)
    {
        PlayGamesPlatform.Instance.Authenticate(async (status) =>
        {
            if (status == SignInStatus.Success)
            {
                Debug.Log("Login via google play games successful");

                PlayGamesPlatform.Instance.RequestServerSideAccess(true, code =>
                {
                    Debug.Log($"Auth code : {code}");
                    m_GooglePlayGamesAuthToken = code;
                });

                await SignInViaGooglePlayGamesAsync(m_GooglePlayGamesAuthToken, OnSignIn);
            }
            else
            {
                Debug.Log($"GooglePlayGames login unsuccessful, status: {status}");
            }
        });
    }



    public void TryToSignInViaGooglePlayGames(Action OnSignIn)
    {
        if (!PlayGamesPlatform.Instance.IsAuthenticated())
        {
            Debug.LogWarning("Not yet authenticated via Google Play Games -- attempting login again");
            LoginViaGooglePlayGames(OnSignIn);
            return;
        }

        SignInOrLinkWithGooglePlayGames(OnSignIn);
    }

    /*
    public void StartSignInViaGooglePlayGames()
    {
        if (!PlayGamesPlatform.Instance.IsAuthenticated())
        {
            Debug.LogWarning("Not yet authenticated via Google Play Games -- attempting login again");
            LoginViaGooglePlayGames();
            return;
        }

        SignInOrLinkWithGooglePlayGames();
    }
    */

    private async void SignInOrLinkWithGooglePlayGames(Action OnSignIn)
    {
        if (string.IsNullOrEmpty(m_GooglePlayGamesAuthToken))
        {
            Debug.LogWarning("Autherization code is null or empty");
            return;
        }

        if (!AuthenticationService.Instance.IsSignedIn)
        {
            await SignInViaGooglePlayGamesAsync(m_GooglePlayGamesAuthToken, OnSignIn);
        }
        else
        {
            await LinkViaGooglePlayGamesAsync(m_GooglePlayGamesAuthToken);
        }
    }

    private async Task SignInViaGooglePlayGamesAsync(string authCode, Action OnSignIn)
    {
        try
        {
            await AuthenticationService.Instance.SignInWithGooglePlayGamesAsync(authCode);
            OnSignIn?.Invoke();
            Debug.Log("Sing in via google succwssfully");

        } catch (AuthenticationException ex)
        {
            Debug.LogException(ex);
        }
    }

    private async Task LinkViaGooglePlayGamesAsync(string authCode)
    {
        try
        {
            await AuthenticationService.Instance.LinkWithGooglePlayGamesAsync(authCode);
            Debug.Log("Link is successful");
        } catch (AuthenticationException ex) when (ex.ErrorCode == AuthenticationErrorCodes.AccountAlreadyLinked)
        {
            Debug.LogException(ex);
        }
    }
}
