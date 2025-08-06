using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePointsManager : MonoBehaviour
{
    [SerializeField] private int points;

    [SerializeField] private SaveSystemManager saveManager;

    private void Awake()
    {
        //saveManager = FindAnyObjectByType<SaveSystemManager>();
    }

    private void Start()
    {
        saveManager = FindAnyObjectByType<SaveSystemManager>();
    }

    public void IncreasePoints(int point)
    {
        points += point;
    }

    public SaveSystemManager GetTestSave ()
    {
        return saveManager;
    }

    public int GetPoints() { return points; }

    public void SavePoints()
    {
        if (saveManager == null) return;
        saveManager.AddPoints(points);
        saveManager.Save();
    }
}
