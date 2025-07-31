using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUIManager : MonoBehaviour
{
    [SerializeField] private EnterScreenUIManager enterScreenUIManager;
    [SerializeField] private MenuScreenUIManager menuScreenUIManager;
    [SerializeField] private TopBarUIManager topBarUIManager;
    [SerializeField] private CharacterSelectionUIManager characterSelectionUIManager;
    [SerializeField] private SettingsUIManager settingsUIManager;


    private void Awake()
    {
        enterScreenUIManager.OpenUI(() =>
        {
            menuScreenUIManager.CloseUI();
            topBarUIManager.CloseUI();
            characterSelectionUIManager.CloseUI();
            settingsUIManager.CloseUI();
        });
    }

    public static event Action<int> SwitchCharacters;
    public static event Action OnSelectorOpened;
    public static event Action OnGoPlay;
    public static event Action OnFinalVolumeSet;
    public static event Action<float> OnVolumeSet;
    public static event Action<bool> OnVibrationSet;
    public static event Action OnBuyCharacter;
    public static event Action OnSelectCharacter;
    public static event Action OnSelectorClosed;
    public static event Action OnCharacterSwitched;

    private void OnEnable()
    {
        enterScreenUIManager.EnterInMenu += EnterInMenu;
        menuScreenUIManager.OpenCharacterSelectorUI += OpenCharacterSelection;
        menuScreenUIManager.OpenSettingsUI += OpenSettings;
        menuScreenUIManager.Play += GoPlay;
        settingsUIManager.CloseOnClick += CloseSettings;
        settingsUIManager.OnFinalVolumeValue += FinalVolumeValueSet;
        settingsUIManager.OnVibrationChanged += VibrationChanged;
        settingsUIManager.OnVolumeChanged += VolumeChanged;
        characterSelectionUIManager.CloseOnClick += CloseCharacterSelector;
        characterSelectionUIManager.SwitchToNext += SwitchUIToNextCharacter;
        characterSelectionUIManager.SwitchToPrevious += SwitchUIToPreviousCharacter;
        characterSelectionUIManager.OnBuy += BuyCharacter;
        characterSelectionUIManager.OnSelect += SelectCharacter;
        MenuCharacter.OnAnimationActivated += MakeButtonsUninteractable;
        MenuCharacter.OnAnimationFinished += MakeButtonsInteractable;
        CharacterShopManager.OnSentCharacterInfoToUI += SetCharacterInfoToUI;
        CharacterShopManager.OnSelectedCharacterSwitched += ChangeSelectButtonState;
        CharacterShopManager.OnOpenCharacterSwitched += OpenCloseBuySelectButtons;
        CharacterShopManager.OnPurchaseableCharacterSwitched += ChageBuyButtonState;
        MenuPointsTransactionManager.ChangeUIPoints += ChangeToBarPointsUIInfo;
    }

    private void OnDisable()
    {
        enterScreenUIManager.EnterInMenu -= EnterInMenu;
        menuScreenUIManager.OpenCharacterSelectorUI -= OpenCharacterSelection;
        menuScreenUIManager.OpenSettingsUI -= OpenSettings;
        menuScreenUIManager.Play -= GoPlay;
        settingsUIManager.CloseOnClick -= CloseSettings;
        settingsUIManager.OnFinalVolumeValue -= FinalVolumeValueSet;
        settingsUIManager.OnVibrationChanged -= VibrationChanged;
        settingsUIManager.OnVolumeChanged -= VolumeChanged;
        characterSelectionUIManager.CloseOnClick -= CloseCharacterSelector;
        characterSelectionUIManager.SwitchToNext -= SwitchUIToNextCharacter;
        characterSelectionUIManager.SwitchToPrevious -= SwitchUIToPreviousCharacter;
        characterSelectionUIManager.OnBuy -= BuyCharacter;
        characterSelectionUIManager.OnSelect -= SelectCharacter;
        MenuCharacter.OnAnimationActivated -= MakeButtonsUninteractable;
        MenuCharacter.OnAnimationFinished -= MakeButtonsInteractable;
        CharacterShopManager.OnSentCharacterInfoToUI -= SetCharacterInfoToUI;
        CharacterShopManager.OnSelectedCharacterSwitched -= ChangeSelectButtonState;
        CharacterShopManager.OnOpenCharacterSwitched -= OpenCloseBuySelectButtons;
        CharacterShopManager.OnPurchaseableCharacterSwitched -= ChageBuyButtonState;
        MenuPointsTransactionManager.ChangeUIPoints -= ChangeToBarPointsUIInfo;
    }

    private void EnterInMenu()
    {
        enterScreenUIManager.CloseUI(() =>{

            menuScreenUIManager.OpenUI();
            topBarUIManager.OpenUI();
        });
    }

    private void OpenCharacterSelection()
    {
        menuScreenUIManager.CloseUI(() =>
        {
            characterSelectionUIManager.OpenUI();
            OnSelectorOpened?.Invoke();
        });
    }

    private void CloseCharacterSelector()
    {
        characterSelectionUIManager.CloseUI(() =>
        {
            menuScreenUIManager.OpenUI();
            OnSelectorClosed?.Invoke();
        });
    }

    private void SwitchUIToNextCharacter()
    {
        SwitchCharacters?.Invoke(1);
    }

    private void SwitchUIToPreviousCharacter()
    {
        SwitchCharacters?.Invoke(-1);
    }

    private void OpenSettings()
    {
       settingsUIManager.OpenUI();
    }

    private void CloseSettings()
    {
        settingsUIManager.CloseUI();
    }

    private void GoPlay()
    {
        OnGoPlay?.Invoke();
    }

    private void FinalVolumeValueSet()
    {
        OnFinalVolumeSet?.Invoke();
    }

    private void VibrationChanged(bool state)
    {
        OnVibrationSet?.Invoke(state);
    }

    private void VolumeChanged(float volume)
    {
        OnVolumeSet?.Invoke(volume);
    }

    private void BuyCharacter()
    {
        OnBuyCharacter?.Invoke();
    }

    private void SelectCharacter()
    {
        OnSelectCharacter?.Invoke();
    }

    private void SetCharacterInfoToUI(int priceInfo, string nameInfo)
    {
        characterSelectionUIManager.SetTextToCharacterPrice(priceInfo.ToString());
        characterSelectionUIManager.SetTextToCharacterName(nameInfo);
    }

    private void MakeButtonsUninteractable()
    {
        characterSelectionUIManager.MakeBuyButtonUninteractable();
        characterSelectionUIManager.MakeSelectButtonUninteractable();
        characterSelectionUIManager.MakeSwitchButtonsUninteractable();
        characterSelectionUIManager.MakeMenuButtonUninteractable();
        Debug.Log("sss0");
    }

    private void MakeButtonsInteractable()
    {
        characterSelectionUIManager.MakeBuyButtonInteractable();
        characterSelectionUIManager.MakeSelectButtonInteractable();
        characterSelectionUIManager.MakeSwitchButtonsInteractable();
        characterSelectionUIManager.MakeMenuButtonInteractable();
        Debug.Log("sss00");
        OnCharacterSwitched?.Invoke();
    }

    private void OpenCloseBuySelectButtons(bool state)
    {
        if (state) {
            //characterSelectionUIManager.DeactivateSelectButton();
            //characterSelectionUIManager.ActivateBuyButton();
            characterSelectionUIManager.ActivateSelectButton();
            characterSelectionUIManager.DeactivateBuyButton();
        } else
        {
            //characterSelectionUIManager.ActivateSelectButton();
            //characterSelectionUIManager.DeactivateBuyButton();
            characterSelectionUIManager.DeactivateSelectButton();
            characterSelectionUIManager.ActivateBuyButton();
        }
        Debug.Log("sss1");
    }

    private void ChangeSelectButtonState(bool state)
    {
        if(state)
        {
            characterSelectionUIManager.MakeSelectButtonUninteractable();
        } else
        {
            characterSelectionUIManager.MakeSelectButtonInteractable();
        }
        Debug.Log("sss2");
    }

    private void ChageBuyButtonState(bool state)
    {
        if(state)
        {
            characterSelectionUIManager.MakeBuyButtonUninteractable();
        } else
        {
            characterSelectionUIManager.MakeBuyButtonInteractable();
        }
        Debug.Log("sss3");
    }

    private void ChangeToBarPointsUIInfo(int points)
    {
        topBarUIManager.UpdateScoreTextUI(points);
    }
}
