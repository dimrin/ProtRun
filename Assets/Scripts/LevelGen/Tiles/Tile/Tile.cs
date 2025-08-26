using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TileRenderer), typeof(TileItemsHandler))]
public class Tile : MonoBehaviour, ITile {

    [SerializeField] private TileRenderer tileRenderer;
    [SerializeField] private TileItemsHandler tileItemsHandler;
    [SerializeField] private Transform valueItemsHolder;
    [SerializeField] private TileSpawnerObstacleManager obstacleManager;

    public GameObject GameObject => gameObject;

    public static event Action TileExited;

    private void Awake()
    {
        tileRenderer = GetComponent<TileRenderer>();
        tileItemsHandler = GetComponent<TileItemsHandler>();
        obstacleManager = GetComponent<TileSpawnerObstacleManager>();
    }

    public void OnSpawn()
    {
        obstacleManager.ActivateObstacles();
        gameObject.SetActive(true);
        
    }

    public void OnRecycle()
    {
        gameObject.SetActive(false);
        ResetAllChildren();
    }

    private void ResetAllChildren()
    {
        tileRenderer.ResetRenderer(valueItemsHolder);
        tileItemsHandler.ResetPositions();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            TileExited?.Invoke();
            //Debug.Log("Player Crossed");
        }
    }
}
