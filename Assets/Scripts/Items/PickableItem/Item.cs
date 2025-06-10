using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ItemAnimationHandler), typeof(ItemPooler))]
public class Item : MonoBehaviour, IPickable {

    [SerializeField] private ItemAnimationHandler animatorHandler;
    [SerializeField] private ItemPooler pooler;

    private void Awake()
    {
        if (animatorHandler == null) animatorHandler = GetComponent<ItemAnimationHandler>(); 
        if (pooler == null) pooler = GetComponent<ItemPooler>(); 
    }

    public void Picked()
    {
        Debug.Log("Pick");

        OnPicked();
    }

    private void OnPicked()
    {
        animatorHandler.OnPickedAnimationPlay();
        pooler.PoolOut();
        
    }

}
