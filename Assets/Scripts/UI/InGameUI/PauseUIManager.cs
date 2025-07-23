using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUIManager : MonoBehaviour {
    public static event Action Resume;
    public static event Action GoToMenuFromPause;

    public void OpenUI()
    {
        gameObject.SetActive(true);
    }

    public void OpenUI(Action OnOpenedUI)
    {
        OpenUI();
        OnOpenedUI?.Invoke();
    }

    public void CloseUI()
    {
        gameObject.SetActive(false);
    }

    public void CloseUI(Action OnClosedUI)
    {
        CloseUI();
        OnClosedUI?.Invoke();
    }


    public void ResumeOnClick()
    {
        Resume?.Invoke();
    }


    public void GoToMenuOnClick()
    {
        GoToMenuFromPause?.Invoke();
    }
}
