using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectionUIManager : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI characterNameText;
    [SerializeField] private TextMeshProUGUI characterPriceText;

    [SerializeField] private Button buyButton;
    [SerializeField] private Button selectButton;

    [SerializeField] private Button leftSwitchButton;
    [SerializeField] private Button rightSwitchButton;
    [SerializeField] private Button goMenuButton;

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
        buyButton.gameObject.SetActive(true);
    }

    public void DeactivateBuyButton()
    {
        buyButton.gameObject.SetActive(false);
    }

    public void MakeBuyButtonInteractable()
    {
        buyButton.interactable = true;
    }

    public void MakeBuyButtonUninteractable()
    {
        buyButton.interactable = false;
    }

    public void SelectOnClick()
    {
        OnSelect?.Invoke();
    }

    public void ActivateSelectButton()
    {
        selectButton.gameObject.SetActive(true);
    }

    public void DeactivateSelectButton()
    {
        selectButton.gameObject.SetActive(false);
    }

    public void MakeSelectButtonInteractable()
    {
        selectButton.interactable = true;
    }

    public void MakeSelectButtonUninteractable()
    {
        selectButton.interactable = false;
    }

    public void MakeSwitchButtonsInteractable()
    {
        leftSwitchButton.interactable = true;
        rightSwitchButton.interactable = true;
    }

    public void MakeSwitchButtonsUninteractable()
    {
        leftSwitchButton.interactable = false;
        rightSwitchButton.interactable = false;
    }

    public void MakeMenuButtonUninteractable()
    {
        goMenuButton.interactable = false;
    }

    public void MakeMenuButtonInteractable()
    {
        goMenuButton.interactable = true;
    }

    public void CloseUIOnClick()
    {
        CloseOnClick?.Invoke();
    }


}
