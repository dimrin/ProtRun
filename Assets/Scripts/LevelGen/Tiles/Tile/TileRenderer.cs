using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileRenderer : MonoBehaviour
{
    public void ResetRenderer(Transform transform)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
    }


}
