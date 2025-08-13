using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingUIManager : MonoBehaviour
{
    [SerializeField] private LoadingUIAnimationHandler animationHandler;

    public event Action OnAnimationFinished;

    private void Awake()
    {
        if(animationHandler == null) animationHandler = GetComponent<LoadingUIAnimationHandler>();
    }

    public void ActivateCurrentLevelLoadAnimation()
    {
        animationHandler.ActivateCurrentLevelLoadAnimation();
    }

    public void ActivateNewLevelLoadAnimation()
    {
        animationHandler.ActivateNewLevelLoadAnimation();
    }

    public void LoadNewLevelAnimationFinishedOnAnimationEvent()
    {
        OnAnimationFinished?.Invoke();
    }

    public void OpenUI()
    {
        gameObject.SetActive(true);
    }

    public void CloseUI()
    {
        gameObject.SetActive(false);
    }

    public void OpenUI(Action OnUIOpened)
    {
        gameObject.SetActive(true);

        OnUIOpened?.Invoke();
    }


    public void CloseUI(Action OnUIClosed)
    {
        gameObject.SetActive(false);

        OnUIClosed?.Invoke();
    }
}
