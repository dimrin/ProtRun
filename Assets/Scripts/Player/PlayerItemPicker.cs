using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerItemPicker : MonoBehaviour
{

    public void PickItem(GameObject item)
    {
        if(item.TryGetComponent(out IPickable pickableItem))
        {
            pickableItem.Picked();
        }
    }
}
