using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectionUIManager : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI characterNameText;
    [SerializeField] private TextMeshProUGUI characterPriceText;

    [SerializeField] private Button BuyButton;
    [SerializeField] private Button SelectButton;

    public event Action SwitchToPrevious;
    public event Action SwitchToNext;
    public event Action CloseOnClick;
    public event Action OnBuy;
    public event Action OnSelect;

    public void SwitchPreviousOnClick()
    {
        SwitchToPrevious?.Invoke();
    }

    public void SwitchNextOnClick()
    {
        SwitchToNext.Invoke();
    }

    public void OpenUI()
    {
        gameObject.SetActive(true);
    }

    public void OpenUI(Action OnUIOpened)
    {
        gameObject.SetActive(true);

        OnUIOpened?.Invoke();
    }

    public void SetTextToCharacterName(string nameText)
    {
        characterNameText.text = nameText;
    }

    public void SetTextToCharacterPrice(string priceText)
    {
        characterPriceText.text = priceText;
    }

    public void CloseUI()
    {
        gameObject.SetActive(false);
    }

    public void CloseUI(Action OnUIClosed)
    {
        gameObject.SetActive(false);

        OnUIClosed?.Invoke();
    }

    public void BuyOnClick()
    {
        OnBuy?.Invoke();
    }

    public void ActivateBuyButton()
    {
        BuyButton.gameObject.SetActive(true);
    }

    public void DeactivateBuyButton()
    {
        BuyButton.gameObject.SetActive(false);
    }

    public void MakeBuyButtonInteractable()
    {
        BuyButton.interactable = true;
    }

    public void MakeBuyButtonUninteractable()
    {
        BuyButton.interactable = false;
    }

    public void SelectOnClick()
    {
        OnSelect?.Invoke();
    }

    public void ActivateSelectButton()
    {
        SelectButton.gameObject.SetActive(true);
    }

    public void DeactivateSelectButton()
    {
        SelectButton.gameObject.SetActive(false);
    }

    public void MakeSelectButtonInteractable()
    {
        SelectButton.interactable = true;
    }

    public void MakeSelectButtonUninteractable()
    {
        SelectButton.interactable = false;
    }

    public void CloseUIOnClick()
    {
        CloseOnClick?.Invoke();
    }
}
