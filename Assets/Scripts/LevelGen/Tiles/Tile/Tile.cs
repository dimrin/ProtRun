using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TileRenderer), typeof(TileItemsHandler))]
public class Tile : MonoBehaviour, ITile {

    [SerializeField] private TileRenderer tileRenderer;
    [SerializeField] private TileItemsHandler tileItemsHandler;

    public GameObject GameObject => gameObject;

    public static event Action TileExited;

    private void Awake()
    {
        tileRenderer = GetComponent<TileRenderer>();
        tileItemsHandler = GetComponent<TileItemsHandler>();
    }

    public void OnSpawn()
    {
        gameObject.SetActive(true);
        
    }

    public void OnRecycle()
    {
        gameObject.SetActive(false);
        ResetAllChildren();
    }

    private void ResetAllChildren()
    {
        tileRenderer.ResetRenderer();
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
