using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSessionManager : MonoBehaviour
{
    [SerializeField] private GamePointsManager gamePointsManager;

    public static event Action<int> PointsIncreased;

    private void Awake()
    {
        if(gamePointsManager == null) gamePointsManager = FindAnyObjectByType<GamePointsManager>();
    }


    private void OnEnable()
    {
        Player.OnPickedPoint += IncreasePoints;
    }

    private void OnDisable()
    {
        Player.OnPickedPoint -= IncreasePoints;
    }


    private void IncreasePoints(int point)
    {
        gamePointsManager.IncreasePoints(point);
        SetPointsToUI();
    }

    private void SetPointsToUI()
    {
        PointsIncreased?.Invoke(gamePointsManager.GetPoints());
    }

}
