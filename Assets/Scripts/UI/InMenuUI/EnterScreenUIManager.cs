using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterScreenUIManager : MonoBehaviour
{
    public event Action EnterInMenu;

    public void EnterOnClick()
    {
        EnterInMenu?.Invoke();
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
