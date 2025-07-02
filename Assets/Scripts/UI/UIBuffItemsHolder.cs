using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIBuffItemsHolder : MonoBehaviour {
    [SerializeField] private List<UIBuffItem> UIItems = new List<UIBuffItem>();

    private Action RunBuffUITimer;

    private void Awake()
    {
        if (UIItems.Count == 0)
        {
            Debug.LogError("Didnt assing items");
        }
    }


    public void ActivateBuffUI(ItemType itemType, int timeValue)
    {
        foreach (var item in UIItems) { 
            if(item.ItemType == itemType)
            {
                if (item.IsActive())
                {
                    HandleBuffUITimerFinished(item);
                }
                item.ActivateBuffTimer(timeValue);
                RunBuffUITimer += item.RunBuffTimer;
                item.OnTimerFinished += HandleBuffUITimerFinished;
                break;
            }
        }
    }

    public void UpdateBuffTimer()
    {
        RunBuffUITimer?.Invoke();
    }

    private void HandleBuffUITimerFinished(UIBuffItem item)
    {
        RunBuffUITimer -= item.RunBuffTimer;
        item.OnTimerFinished -= HandleBuffUITimerFinished;
    }
}
