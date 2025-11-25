using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveFileLoaderManager : MonoBehaviour {
    [SerializeField] private SaveSystemManager saveSystemManager;
    [SerializeField] private MenuPointsTransactionManager menuPointsManager;

    public static event Action DataLoaded;

    /*
    private void Awake()
    {
        if (saveSystemManager == null)
        {
            saveSystemManager = FindAnyObjectByType<SaveSystemManager>();
        }

        if (menuPointsManager == null)
        {
            menuPointsManager = FindAnyObjectByType<MenuPointsTransactionManager>();
        }

        LoadSaveFile();
    }
    */



    private void Start()
    {
        SetDataOnEnterMenu();
    }

    public void LoadGameSave()
    {
        Initialize();
        LoadSaveFile();
    }

    private void Initialize()
    {
        if (saveSystemManager == null)
        {
            saveSystemManager = FindAnyObjectByType<SaveSystemManager>();
        }

        if (menuPointsManager == null)
        {
            menuPointsManager = FindAnyObjectByType<MenuPointsTransactionManager>();
        }
    }

    private void LoadSaveFile()
    {
        Debug.Log("Load State " + saveSystemManager.IsLoaded());

        if (!saveSystemManager.IsLoaded())
        {
            saveSystemManager.Load();
            Debug.Log("Loaded File");
            DataLoaded?.Invoke();
        } else
        {
            DataLoaded?.Invoke();
            Debug.Log("Invoke On loaded");
        }
    }

    private void SetDataOnEnterMenu()
    {
        if (saveSystemManager.IsLoaded())
        {
            DataLoaded?.Invoke();
            Debug.Log("Invoke On loaded 2 ");
        }
    }
}
