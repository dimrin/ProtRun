using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileItemsHandler : MonoBehaviour
{
    private Dictionary<GameObject, Vector3> pickableObjectsDefaultPositions = new Dictionary<GameObject, Vector3>();

    private void Awake()
    {
        //CollectDefaultPositions();
        Debug.Log("AwakeHandler");
    }

    public void CollectDefaultPositions(Transform itemsParantTransform)
    {
        foreach (Transform pickableChild in itemsParantTransform)
        {
            if (pickableChild.TryGetComponent<IPickable>(out _))
            {
                pickableObjectsDefaultPositions[pickableChild.gameObject] = pickableChild.localPosition;
            }
        }
    }

    public void ResetPositions()
    {
        foreach (KeyValuePair<GameObject, Vector3> valuePair in pickableObjectsDefaultPositions)
        {
            valuePair.Key.transform.localPosition = valuePair.Value;
        }
    }
}
