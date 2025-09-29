using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TileSingleObstacleSpawner : MonoBehaviour {
    [SerializeField] private List<Obstacle> obstacles = new List<Obstacle>();

    //private bool isActivated = false;

    //[SerializeField] private GameObject currentObstacle;
    //[SerializeField] private Obstacle currentObstacleItem;

    private void Awake()
    {
        foreach (Transform obstacle in transform)
        {
            obstacle.gameObject.SetActive(false);
            obstacles.Add(obstacle.GetComponent<Obstacle>());
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

        //currentObstacle = obstacles[randomIndex].GetObstacleObject();
        //currentObstacleItem = obstacles[randomIndex];

        //obstacles[randomIndex].GetObstacleObject().SetActive(true);

        obstacles[randomIndex].ActiveItself();

        //currentObstacle.SetActive(true);
        //currentObstacleItem.GetObstacleObject().SetActive(true);

        Debug.Log("ActivatedObstacle");
        //isActivated = true;
    }

    public void DeactivateObstacle()
    {

        foreach (Obstacle obstacle in obstacles)
        {
            //obstacle.gameObject.SetActive(false);
            if (obstacle.gameObject.activeSelf)
            {
                obstacle.DeactivateItself();
                return;
            }

        }


        //currentObstacleItem.DeactivateItself();

        //currentObstacle.SetActive(false);
        //isActivated = false;
    }

    /*
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

        foreach (GameObject obstacle in obstacles)
        {
            obstacle.SetActive(false);
        }


        //currentObstacle.SetActive(false);
        //isActivated = false;
    }
    */
}
