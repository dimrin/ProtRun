using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FinalUIManager : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI finalPointsText;

    public static event Action GoToMenu;
    public static event Action ReviveOnAd;

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
}
