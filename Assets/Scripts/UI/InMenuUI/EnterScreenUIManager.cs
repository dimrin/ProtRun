using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnterScreenUIManager : MonoBehaviour {
    [SerializeField] private float timerTime = 2f;

    //[SerializeField] private Button enterButton;

    [SerializeField] private EnterScreenUIAnimationHandler enterScreenUIAnimationHandler;

    public event Action EnterInMenu;

    private void Awake()
    {
        if (enterScreenUIAnimationHandler == null) enterScreenUIAnimationHandler = GetComponent<EnterScreenUIAnimationHandler>();
    }

    public void ActivateLoadingAnimation()
    {
        StartCoroutine(GoAnimationCoroutine(timerTime));
    }


    private IEnumerator GoAnimationCoroutine(float timeToSet)
    {
        enterScreenUIAnimationHandler.StartLoadingAnimation();
        yield return new WaitForSeconds(timeToSet);
        /*
        enterScreenUIAnimationHandler.StopLoadingAnimation(() =>
        {
            // enterButton.gameObject.SetActive(true);
            enterScreenUIAnimationHandler.StartEnterAnimation();
        });
        */
        enterScreenUIAnimationHandler.StartEnterAnimation();
    }

    public void EnterOnClick()
    {
        EnterInMenu?.Invoke();

        /*
        enterScreenUIAnimationHandler.StopEnterAnimation(() =>
        {
            EnterInMenu?.Invoke();
        });
        */
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
