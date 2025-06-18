using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTileRecycler : MonoBehaviour {
    public void RecycleTileOnExit(Queue<ITile> activeTiles, Action OnTileRecycled)
    {
        if (activeTiles.Count > 0)
        {
            ITile oldestTile = activeTiles.Dequeue();
            oldestTile.OnRecycle();
            OnTileRecycled?.Invoke();
        }
    }
}
