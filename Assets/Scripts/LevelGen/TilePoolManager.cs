using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class TilePoolManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> tilePrefabs;

    private List<ITile> allTiles = new List<ITile>();

    public List<ITile> AllTiles => allTiles;

    private void Awake()
    {
        foreach (GameObject prefab in tilePrefabs)
        {
            GameObject tileGO = Instantiate(prefab, transform);
            ITile tile = tileGO.GetComponent<ITile>();
            tile.OnRecycle();
            allTiles.Add(tile);
        }
    }

    public ITile GetInactiveRandomTile()
    {
        List<ITile> inactive = allTiles.FindAll(t => !t.GameObject.activeSelf);
        if (inactive.Count == 0) return null;
        return inactive[Random.Range(0, inactive.Count)];
    }
}
