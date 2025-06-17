using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPooler : MonoBehaviour
{
    public void PoolOut()
    {
        gameObject.SetActive(false);
    }
}
