using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBuffItem : MonoBehaviour {
    [SerializeField] private ItemType itemType;

    [SerializeField] private Image timerBar;

    [SerializeField] private float timerDuration;

    private float maxTimerTime;

    private bool _isActive = false;

    public event Action<UIBuffItem> OnTimerFinished;

    private void Awake()
    {
        //Debug.Log("ItemUI");
    }

    public void RunBuffTimer()
    {
        if (_isActive)
        {

            if (timerDuration >= 0)
            {
                timerDuration -= Time.deltaTime;
                timerBar.fillAmount = timerDuration / maxTimerTime;
            }
            else
            {
                _isActive = false;
                OnTimerFinished?.Invoke(this);
                gameObject.SetActive(false);
            }

        }
    }


    public void ActivateBuffTimer(float timerTime)
    {
        gameObject.SetActive(true);
        _isActive = true;
        maxTimerTime = timerTime;
        timerDuration = maxTimerTime;
    }


    public ItemType ItemType => itemType;
}
