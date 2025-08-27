using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TileSingleObstacleSpawner : MonoBehaviour {
    [SerializeField] private List<GameObject> obstacles = new List<GameObject>();

    private bool isActivated = false;

    [SerializeField] private GameObject currentObstacle;

    private void Awake()
    {
        foreach (Transform obstacle in transform)
        {
            obstacle.gameObject.SetActive(false);
            obstacles.Add(obstacle.gameObject);
            //isActivated = false;
        }

        Debug.Log("Awake");
    }
    
    
    private void OnEnable()
    {
        
        Debug.Log("ONena");
    }
    

    public void ActivateRandomObstacle()
    {
        //if (isActivated) return;
        int randomIndex = Random.Range(0, obstacles.Count);

        currentObstacle = obstacles[randomIndex];

        currentObstacle.SetActive(true);

        Debug.Log("ActivatedObstacle");
        //isActivated = true;
    }

    public void DeactivateObstacle()
    {
        
        foreach(GameObject obstacle in obstacles)
        {
            obstacle.SetActive(false);
        }
        

        //currentObstacle.SetActive(false);
        //isActivated = false;
    }
}
