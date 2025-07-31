using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveFileLoaderManager : MonoBehaviour
{
    [SerializeField] private SaveSystemManager saveSystemManager;
    [SerializeField] private MenuPointsTransactionManager menuPointsManager;

    public static event Action DataLoaded;

    private void Awake()
    {
        if (saveSystemManager == null)
        {
            saveSystemManager = FindAnyObjectByType<SaveSystemManager>();
            Debug.Log("FOund");
        }

        if (menuPointsManager == null)
        {
            menuPointsManager = FindAnyObjectByType<MenuPointsTransactionManager>();
            Debug.Log("FOund 2 ");
        }

        LoadSaveFile();
    }


    private void LoadSaveFile()
    {
        saveSystemManager.Load();
        if (saveSystemManager.IsLoaded())
        {
            DataLoaded?.Invoke();
        }
    }
}
