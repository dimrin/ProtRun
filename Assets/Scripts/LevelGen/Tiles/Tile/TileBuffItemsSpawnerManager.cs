using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBuffItemsSpawnerManager : MonoBehaviour {
    [SerializeField] private List<TileBuffItemSpawner> buffItemSpawners = new List<TileBuffItemSpawner>();


    public void TryToSpawnBuffs()
    {
        if(buffItemSpawners.Count > 0)
        {
            foreach (TileBuffItemSpawner buffItemSpawner in buffItemSpawners)
            {
                buffItemSpawner.TryToSpawnRandomBuffItem();
            }
        }
    }

    /*
    public void DeactivateBuffs()
    {
        foreach (TileBuffItemSpawner buffItemSpawner in buffItemSpawners)
        {
            buffItemSpawner.DeactivateBuff();
        }
    }
    */


}
