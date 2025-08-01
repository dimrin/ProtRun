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
        MenuUIManager.OnGoPlay += LoadGame;
    }

    private void OnDisable()
    {
        MenuUIManager.OnGoPlay -= LoadGame;
    }

    private void LoadGame()
    {
        levelLoader.LoadGameLevel();
    }
}
