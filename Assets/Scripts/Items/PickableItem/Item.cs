using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ItemAnimationHandler), typeof(ItemPooler))]
public class Item : MonoBehaviour, IPickable {

    [SerializeField] private ItemType type;

    [SerializeField][Range(0, 15)] private int value = 1;

    [SerializeField] private ItemAnimationHandler animatorHandler;
    [SerializeField] private ItemPooler pooler;

    private void Awake()
    {
        if (animatorHandler == null) animatorHandler = GetComponent<ItemAnimationHandler>();
        if (pooler == null) pooler = GetComponent<ItemPooler>();
    }

    public void Picked()
    {
        OnPicked();
    }


    private void OnPicked()
    {
        animatorHandler.OnPickedAnimationPlay();
        pooler.PoolOut();
    }

    public int GetValue() { return value; }

    public ItemType GetItemType () { return type; }
}

[Serializable]
public enum ItemType { 
    Value,
    Shield,
    Ultimate,
    Magnet
}



