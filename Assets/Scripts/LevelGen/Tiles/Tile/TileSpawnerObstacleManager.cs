using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawnerObstacleManager : MonoBehaviour
{
    
    [SerializeField] private List<TileSingleObstacleSpawner> obstaclesSpawners = new List<TileSingleObstacleSpawner>();


    public void ActivateObstacles()
    {
        foreach(TileSingleObstacleSpawner obstacleSpawner in obstaclesSpawners)
        {
            obstacleSpawner.ActivateRandomObstacle();
        }
    }

    public void DeactivateObstackes()
    {
        foreach (TileSingleObstacleSpawner obstacleSpawner in obstaclesSpawners)
        {
            obstacleSpawner.DeactivateObstacle();
        }
    }
    
    
}
