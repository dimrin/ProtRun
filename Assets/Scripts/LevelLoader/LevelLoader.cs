using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public static event Action OnLevelLoaded;

    private AsyncOperation loadingOperation;

    private bool canActivateScene = false;

    private void Awake()
    {
        OnLevelLoaded?.Invoke();
    }

    public void LoadMainMenu()
    {
        LoadScene(0);
    }

    public void LoadGameLevel()
    {
        LoadScene(1);
    }

    private void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    private void LoadSceneAsync(int index)
    {
        SceneManager.LoadSceneAsync(index);
    }

    public void LoadGameLevelInBackground()
    {
        //LoadSceneAsync(1);
        StartCoroutine(LoadSceneInBackground(1));
    }

    public void LoadMainMenuInBackground()
    {
        //LoadSceneAsync(0);
        StartCoroutine(LoadSceneInBackground(0));
    }

    public void ActivateLoadedScene()
    {
        canActivateScene = true;
    }

    private IEnumerator LoadSceneInBackground(int index)
    {
        // Start loading but don't activate yet
        loadingOperation = SceneManager.LoadSceneAsync(index);
        loadingOperation.allowSceneActivation = false;

        // Wait until the scene is loaded to 90% (ready to activate)
        while (loadingOperation.progress < 0.9f)
        {
            Debug.Log($"Loading progress: {loadingOperation.progress * 100f}%");
            yield return null;
        }

        Debug.Log("Scene is ready but waiting for canActivate...");

        // Wait until canActivate becomes true
        while (!canActivateScene)
        {
            yield return null;
        }

        // Activate the scene
        loadingOperation.allowSceneActivation = true;
        canActivateScene = false;
    }

}
