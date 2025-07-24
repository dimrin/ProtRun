using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScreenUIManager : MonoBehaviour
{

    public event Action OpenSettingsUI;

    public event Action OpenCharacterSelectorUI; 

    public event Action Play;


    public void OpenCharacterSelectorUIOnClick()
    {
        OpenCharacterSelectorUI?.Invoke();
    }

    public void OpenSettingsUIOnClick()
    {
        OpenSettingsUI?.Invoke();
    }


    public void PlayOnCLick()
    {
        Play?.Invoke();
    }

    public void OpenMainScreenUI()
    {
        gameObject.SetActive(true);
    }

    public void CloseMainScreenUI()
    {
        gameObject.SetActive(false);
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
