using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePointsManager : MonoBehaviour
{
    [SerializeField] private int points;


    public void IncreasePoints(int point)
    {
        points += point;
    }

    public int GetPoints() { return points; }

}
