using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePointsManager : MonoBehaviour
{
    [SerializeField] private int points;

    [SerializeField] private SaveManager saveManager;

    private void Awake()
    {
        saveManager = FindAnyObjectByType<SaveManager>();
    }

    public void IncreasePoints(int point)
    {
        points += point;
    }

    public int GetPoints() { return points; }

    public void SavePoints()
    {
        saveManager.AddPoints(points);
        saveManager.Save();
    }
}
