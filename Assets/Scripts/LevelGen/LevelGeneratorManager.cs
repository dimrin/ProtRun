using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LevelTileRecycler), typeof(LevelTileSpawner))]
public class LevelGeneratorManager : MonoBehaviour {
    [SerializeField] private TilePoolManager tilePoolManager;
    [SerializeField] private float tileLength = 30f;
    [SerializeField] private int tilesInRow = 3;
    [SerializeField] private float startSpawnZPosition;

    [SerializeField] private LevelTileRecycler tileRecycler;
    [SerializeField] private LevelTileSpawner tileSpawner;

    private readonly Queue<ITile> activeTiles = new Queue<ITile>();
    private float nextSpawnZ = 0f;

    private void Awake()
    {
        tileRecycler = GetComponent<LevelTileRecycler>();
        tileSpawner = GetComponent<LevelTileSpawner>();
        if(tilePoolManager == null ) tilePoolManager = FindAnyObjectByType<TilePoolManager>();
    }

    private void OnEnable()
    {
        Tile.TileExited += HandleTileExit;
    }

    private void OnDisable()
    {
        Tile.TileExited -= HandleTileExit;
    }

    private void Start()
    {
        nextSpawnZ = startSpawnZPosition;

        for (int i = 0; i < tilesInRow; i++)
        {
            SpawnNextTile();
        }
    }
    
    private void SpawnNextTile()
    {
        ITile tile = tilePoolManager.GetInactiveRandomTile();
        Transform tileParent = transform;
        tileSpawner.SpawnTile(tile, nextSpawnZ, tileParent, () =>
        {
            nextSpawnZ += tileLength;
            activeTiles.Enqueue(tile);
        });
    }
    
    private void HandleTileExit()
    {
        tileRecycler.RecycleTileOnExit(activeTiles, () => {
            SpawnNextTile();
        });
    }

}
