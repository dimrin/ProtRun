using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TileRenderer), typeof(TileItemsHandler))]
public class TileManager : MonoBehaviour, ITile {

    [SerializeField] private TileRenderer tileRenderer;
    [SerializeField] private TileItemsHandler tileItemsHandler;
    [SerializeField] private Transform valueItemsHolder;
    [SerializeField] private TileSpawnerObstacleManager obstacleManager;
    [SerializeField] private TileBuffItemsSpawnerManager buffItemManager;

    public GameObject GameObject => gameObject;

    public static event Action TileExited;

    private void Awake()
    {
        tileRenderer = GetComponent<TileRenderer>();
        tileItemsHandler = GetComponent<TileItemsHandler>();
        obstacleManager = GetComponent<TileSpawnerObstacleManager>();
        buffItemManager = GetComponent<TileBuffItemsSpawnerManager>();
        tileItemsHandler.CollectDefaultPositions(valueItemsHolder);
    }

    public void OnSpawn()
    {
        obstacleManager.ActivateObstacles();
        buffItemManager.TryToSpawnBuffs();
        gameObject.SetActive(true);
        Debug.Log("Spawn");
    }

    public void OnRecycle()
    {

        gameObject.SetActive(false);
        ResetAllChildren();
    }

    private void ResetAllChildren()
    {
        tileRenderer.ResetRenderer(valueItemsHolder);
        obstacleManager.DeactivateObstackes();
        //buffItemManager.DeactivateBuffs();
        tileItemsHandler.ResetPositions();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            TileExited?.Invoke();
        }
    }
}
