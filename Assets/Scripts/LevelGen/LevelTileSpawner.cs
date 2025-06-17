using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LevelTileSpawner : MonoBehaviour
{
    public void SpawnTile(ITile tile, float spawnZOffset, Transform tileParent, Action OnSpawnTile)
    {
        if (tile == null) return;

        tile.GameObject.transform.SetParent(tileParent);
        tile.GameObject.transform.position = new Vector3(0, 0, spawnZOffset);
        tile.OnSpawn();
        OnSpawnTile?.Invoke();
    }

}
