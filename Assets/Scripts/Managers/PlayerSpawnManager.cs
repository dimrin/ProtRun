using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnManager : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;

    [SerializeField] private Vector3 spawnRotationOffset = new Vector3(0, 0, 0);

    public GameObject SpawnMenuPlayer(GameObject spawnCharacter)
    {
        //return Instantiate(spawnCharacter, spawnPoint.transform.position, Quaternion.Euler(spawnRotationOffset), spawnPoint);
        return Instantiate(spawnCharacter, spawnPoint.transform.position, Quaternion.Euler(spawnRotationOffset));
    }

}
