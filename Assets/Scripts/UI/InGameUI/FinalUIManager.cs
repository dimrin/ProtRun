using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FinalUIManager : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI finalPointsText;

    [SerializeField] private GameObject adReviveWindow;
    [SerializeField] private GameObject finalWindow;

    public static event Action GoToMenu;
    public static event Action ReviveOnAd;
    public static event Action CloseReviveWindow;

    private void Awake()
    {
        if (finalPointsText == null)
        {
            Debug.LogWarning($"Forgot assign {finalPointsText.name}");
        }
    }

    public void SetPointsToUIText(int points)
    {
        finalPointsText.text = points.ToString();
    }

    public void OpenAdReviveWindow()
    {
        adReviveWindow.SetActive(true);
    }

    public void CloseAdReviveWindow()
    {
        adReviveWindow.SetActive(false);
    }

    public void OpenFinalWindow()
    {
        finalWindow.SetActive(true);
    }

    public void CloseFinalWindow()
    {
        finalWindow.SetActive(false);
    }

    public void OpenUI()
    {
        gameObject.SetActive(true);
    }

    public void OpenUI(Action OnUiOpened)
    {
        OpenUI();
        OnUiOpened?.Invoke();
    }

    public void CloseUI()
    {
        gameObject.SetActive(false);
    }

    public void CloseUI(Action OnUIClose)
    {
        CloseUI();
        OnUIClose?.Invoke();
    }

    public void GoToMenuOnClick()
    {
        GoToMenu?.Invoke();
    }


    public void ReviveOnAdOnClick()
    {
        ReviveOnAd?.Invoke();
    }

    public void CloseReviveOnClick()
    {
        CloseReviveWindow?.Invoke();  
    }

}
