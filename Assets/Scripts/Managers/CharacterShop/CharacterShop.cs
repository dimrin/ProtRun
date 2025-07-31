using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class CharacterShop : MonoBehaviour
{
    //[SerializeField] private MenuPointsTransactionManager transactionManager;

    //[SerializeField] private SaveSystemManager saveSystemManager;


    //private List<int> openCharacters = new List<int>();

    private void Awake()
    {   /*
        if(transactionManager == null)
        {
            transactionManager = FindAnyObjectByType<MenuPointsTransactionManager>();
        }
        /*
        if(saveSystemManager == null)
        {
            saveSystemManager = FindAnyObjectByType<SaveSystemManager>();
        }

        SetOpenCharactersList(saveSystemManager.GetOpenCharactersList());
        */
    }
    /*
    public void SetOpenCharactersList(List<int> openCharactersList)
    {
        openCharacters = openCharactersList;
    }

    
    public bool IsCurrentCharacterOpened(int index)
    {
        return openCharacters.Contains(index);
    }
    */

    public bool CanBuyCharacter(int characterPrice, int playerMoney)
    {
        return characterPrice >= playerMoney;
    }

    public void BuyCharacter(CharacterSO characterSO, SaveSystemManager saveSystemManager, MenuPointsTransactionManager transactionManager,Action OnBuyCharacter)
    {
        /*
            SaveManager.Instance.RemoveMoney(characterSO.priceInMoney);
            openCharacterList.Add(characterSO.index);
            SaveManager.Instance.AddCharacterToList(characterSO.index);
            SaveManager.Instance.Save();
            buyByMoneyButton.gameObject.SetActive(false);
            buyBySpecailCoinsButton.gameObject.SetActive(false);
            selectButton.gameObject.SetActive(true);
            selectButton.interactable = true;
        */
        transactionManager.RemoveMoney(characterSO.priceInMoney);
        saveSystemManager.SetCharacterToList(characterSO.index);
        saveSystemManager.Save();
        OnBuyCharacter?.Invoke();
    }
}
