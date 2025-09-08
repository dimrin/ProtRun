using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBuffItemSpawner : MonoBehaviour {
    [SerializeField] private int spawnChance = 15;
    [SerializeField] private List<GameObject> buffs = new List<GameObject>();

    private bool isActivated = false;

    [SerializeField] private GameObject activatedBuff;

    private void Awake()
    {
        foreach (Transform buff in transform)
        {
            buff.gameObject.SetActive(false);
            buffs.Add(buff.gameObject);
            //isActivated = false;
        }
    }
    

    
    private void OnEnable()
    {
        Debug.Log("OnEna Buff");
    }
    

    public void TryToSpawnRandomBuffItem()
    {
        if (Random.Range(0, 101) <= spawnChance)
        {
            ActivateRandomBuff();
        }
    }


    private void ActivateRandomBuff()
    {
        //if (isActivated) return;
        int randomIndex = Random.Range(0, buffs.Count);

        activatedBuff = buffs[randomIndex];

        activatedBuff.SetActive(true);

        Debug.Log("name " + buffs[randomIndex].name);
        //isActivated = true;
    }

    /*
    public void DeactivateBuff()
    {
        if (activatedBuff.activeSelf)
        {
            foreach(GameObject buff in buffs)
            {
                buff.SetActive(false);
            }
        }
    }
    */

}
