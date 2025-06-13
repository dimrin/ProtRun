using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclePooler : MonoBehaviour
{
    public void PoolOut()
    {
        Destroy(gameObject);
    }
}
