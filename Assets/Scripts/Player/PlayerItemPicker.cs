using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerItemPicker : MonoBehaviour
{

    public void PickItem(GameObject gameOgject, Action<ItemType, int> OnItemPicked)
    {
        if(gameOgject.TryGetComponent(out IPickable pickableItem))
        {
            if(pickableItem is Item item)
            {
                item.Picked();
                OnItemPicked?.Invoke(item.GetItemType(), item.GetValue());
            }
        }
    }
}
