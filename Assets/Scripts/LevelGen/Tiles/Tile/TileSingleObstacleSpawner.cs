using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSingleObstacleSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> obstacles = new List<GameObject>();

    private void Awake()
    {
        foreach (Transform obstacle in transform)
        {
            obstacle.gameObject.SetActive(false);
            obstacles.Add(obstacle.gameObject);
        }
    }


    public void ActivateRandomObstacle()
    {
        int randomIndex = Random.Range(0, obstacles.Count);

        Debug.Log("Index " + randomIndex);

        obstacles[randomIndex].SetActive(true);
    }
}
