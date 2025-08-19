using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenuManager : MonoBehaviour
{
    [SerializeField] private LevelLoader levelLoader;

    private void Awake()
    {
        if(levelLoader == null) levelLoader = FindAnyObjectByType<LevelLoader>();
    }


    private void OnEnable()
    {
        MenuUIManager.OnPlayPressed += LoadGame;
        MenuUIManager.OnLoadingNewLevelAnimationFinished += ActivateLoadedScene;
        
    }

    private void OnDisable()
    {
        MenuUIManager.OnPlayPressed -= LoadGame;
        MenuUIManager.OnLoadingNewLevelAnimationFinished -= ActivateLoadedScene;
    }

    private void LoadGame()
    {
        //levelLoader.LoadGameLevel();
        levelLoader.LoadGameLevelInBackground();
    }

    private void ActivateLoadedScene()
    {
        levelLoader.ActivateLoadedScene();
    }
}
