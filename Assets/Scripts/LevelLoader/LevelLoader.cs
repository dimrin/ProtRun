using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public static event Action OnLevelLoaded;

    private void Awake()
    {
        OnLevelLoaded?.Invoke();
    }

    private void OnEnable()
    {
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
}
