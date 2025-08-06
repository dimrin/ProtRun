using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPointsTransactionManager : MonoBehaviour
{
    [SerializeField] private SaveSystemManager saveSystemManager;

    [SerializeField] private int money;

    public static event Action<int> ChangeUIPoints;

    private void Awake()
    {
        if(saveSystemManager == null)
        {
            saveSystemManager = FindAnyObjectByType<SaveSystemManager>();
        }
    }

    private void OnEnable()
    {
        SaveFileLoaderManager.DataLoaded += GetPointsOnLoad;
    }

    private void OnDisable()
    {
        SaveFileLoaderManager.DataLoaded -= GetPointsOnLoad;
    }

    private void Start()
    {
        GetPointsOnLoad();
    }

    private void GetPointsOnLoad()
    {
        money = saveSystemManager.GetPoints();

        ChangeUIPoints?.Invoke(money);

    }

    public void AddMoney(int moneyToAdd)
    {
        money += moneyToAdd;
        saveSystemManager.AddPoints(moneyToAdd);
        ChangeUIPoints?.Invoke(money);
    }

    public void RemoveMoney(int moneyToRemove) { 
        money -= moneyToRemove;
        saveSystemManager.RemovePoints(moneyToRemove);
        ChangeUIPoints?.Invoke(money);
    }

    public int GetPlayerMoney()
    {
        return money;
    }
}
