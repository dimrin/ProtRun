using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileRenderer : MonoBehaviour
{
    public void ResetRenderer()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
    }


}
