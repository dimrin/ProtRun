using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnterManager : MonoBehaviour
{
    [SerializeField] private SaveFileLoaderManager saveFileLoaderManager;
    [SerializeField] private AuthenticationManager authenticationManager;
    private void Awake()
    {
        if (saveFileLoaderManager == null) saveFileLoaderManager = FindAnyObjectByType<SaveFileLoaderManager>();
        if (authenticationManager == null) authenticationManager = FindAnyObjectByType<AuthenticationManager>();
        EnterTheGame();
    }

    private void EnterTheGame()
    {
        AuthenticateServices();
        LoadSave();
    }

    private async void AuthenticateServices()
    {
        await authenticationManager.InitializeUnityServices();
    }

    private void LoadSave()
    {
        saveFileLoaderManager.LoadGameSave();
    }
}
