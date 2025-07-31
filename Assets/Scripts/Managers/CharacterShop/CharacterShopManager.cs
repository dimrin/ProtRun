using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterShopManager : MonoBehaviour {
    [SerializeField] private List<CharacterSO> charactersSO = new List<CharacterSO>();

    [SerializeField] private PlayerSpawnInMenuManager spawnInMenuManager;

    [SerializeField] private SaveSystemManager saveSystemManager;

    [SerializeField] private List<Dictionary<int, GameObject>> spawnedList = new List<Dictionary<int, GameObject>>();

    [SerializeField] private CharacterSelector characterSelector;

    [SerializeField] private CharacterShop characterShop;

    [SerializeField] private MenuPointsTransactionManager menuPointsTransactionManager;

    public static event Action DoAnimationOnActivated;
    public static event Action<int, string> OnSentCharacterInfoToUI;
    public static event Action<bool> OnOpenCharacterSwitched;
    public static event Action<bool> OnSelectedCharacterSwitched;
    public static event Action<bool> OnPurchaseableCharacterSwitched;

    private int currentShownCharacterIndex = -1;

    private GameObject currentShownMenuCharacter;
    private CharacterSO currentShownCharacterSO;

    private void Awake()
    {
        if (spawnInMenuManager == null)
        {
            spawnInMenuManager = FindAnyObjectByType<PlayerSpawnInMenuManager>();
        }

        if (saveSystemManager == null)
        {
            saveSystemManager = FindAnyObjectByType<SaveSystemManager>();
        }

        if (characterSelector == null)
        {
            characterSelector = FindAnyObjectByType<CharacterSelector>();
        }

        if (characterShop == null)
        {
            characterShop = FindAnyObjectByType<CharacterShop>();
        }

        if(menuPointsTransactionManager  == null)
        {
            menuPointsTransactionManager = FindAnyObjectByType<MenuPointsTransactionManager>();
        }


        SpawnCharacters();
    }

    private void OnEnable()
    {
        SaveFileLoaderManager.DataLoaded += ActivateSavedCharacter;
        MenuUIManager.SwitchCharacters += SwitchCharacters;
        MenuUIManager.OnSelectorClosed += ActivateSavedCharacter;
        MenuUIManager.OnSelectCharacter += SelectCharacter;
        MenuUIManager.OnBuyCharacter += BuyCharacter;
        MenuUIManager.OnSelectorOpened += CheckingCharacterStatesOnSwitched;
        MenuUIManager.OnCharacterSwitched += CheckingCharacterStatesOnSwitched;
    }

    private void OnDisable()
    {
        SaveFileLoaderManager.DataLoaded -= ActivateSavedCharacter;
        MenuUIManager.SwitchCharacters -= SwitchCharacters;
        MenuUIManager.OnSelectorClosed -= ActivateSavedCharacter;
        MenuUIManager.OnSelectCharacter -= SelectCharacter;
        MenuUIManager.OnBuyCharacter -= BuyCharacter;
        MenuUIManager.OnSelectorOpened -= CheckingCharacterStatesOnSwitched;
        MenuUIManager.OnCharacterSwitched -= CheckingCharacterStatesOnSwitched;
    }

    private void SpawnCharacters()
    {
        foreach (var character in charactersSO)
        {
            var spawnedCharacter = spawnInMenuManager.SpawnMenuPlayer(character.characterForMenu);
            spawnedCharacter.SetActive(false);
            Dictionary<int, GameObject> indexCharacterPair = new Dictionary<int, GameObject>
            {
                { character.index, spawnedCharacter }
            };
            spawnedList.Add(indexCharacterPair);
        }
    }

    private void ActivateSavedCharacter()
    {
        if (currentShownCharacterIndex != saveSystemManager.GetCurrentCharacterIndex())
        {
            currentShownCharacterIndex = saveSystemManager.GetCurrentCharacterIndex();

            DeactivateCurrentCharacter();

            SentCharacterInfoToUI(currentShownCharacterIndex);

            ActivateCharacter(currentShownCharacterIndex, () =>
            {
                CheckShowCharacterStates(currentShownCharacterIndex);
            });
        }


    }


    private void ActivateCharacter(int index, Action OnCharacterActivated)
    {
        foreach (var characterPair in spawnedList)
        {
            if (characterPair.ContainsKey(index))
            {
                characterPair[index].SetActive(true);
                currentShownMenuCharacter = characterPair[index];
                OnCharacterActivated?.Invoke();
                DoAnimationOnActivated?.Invoke();
            }
        }
    }

    private void SentCharacterInfoToUI(int index)
    {
        foreach (var character in charactersSO)
        {
            if (character.index == index)
            {
                currentShownCharacterSO = character;
                OnSentCharacterInfoToUI?.Invoke(character.priceInMoney, character.name);
            }
        }
    }

    private void DeactivateCurrentCharacter()
    {
        if (currentShownMenuCharacter != null)
        {
            currentShownMenuCharacter.SetActive(false);
        }
    }

    private void SwitchCharacters(int index)
    {
        DeactivateCurrentCharacter();


        currentShownCharacterIndex += index;


        if (currentShownCharacterIndex > spawnedList.Count - 1)
        {
            currentShownCharacterIndex = 0;
        }
        else if (currentShownCharacterIndex < 0)
        {
            currentShownCharacterIndex = spawnedList.Count - 1;
        }

        SentCharacterInfoToUI(currentShownCharacterIndex);



        ActivateCharacter(currentShownCharacterIndex, () =>
        {
            CheckShowCharacterStates(currentShownCharacterIndex);
        });
    }

    private void CheckingCharacterStatesOnSwitched()
    {
        CheckShowCharacterStates(currentShownCharacterIndex);
    }

    private void CheckShowCharacterStates(int characterIndexToCheck)
    {
        characterSelector.SetOpenCharactersList(saveSystemManager.GetOpenCharactersList());
        if (characterSelector.IsCurrentCharacterOpened(characterIndexToCheck))
        {
            OnOpenCharacterSwitched?.Invoke(true);
            if (characterSelector.IsCurrentCharacterSelected(characterIndexToCheck, saveSystemManager.GetCurrentCharacterIndex()))
            {
                OnSelectedCharacterSwitched?.Invoke(true);
            } else
            {
                OnSelectedCharacterSwitched?.Invoke(false);
            }

        } else
        {
            OnOpenCharacterSwitched?.Invoke(false);
            if (characterShop.CanBuyCharacter(currentShownCharacterSO.priceInMoney, menuPointsTransactionManager.GetPlayerMoney()))
            {
                OnPurchaseableCharacterSwitched?.Invoke(true);
            } else
            {
                OnPurchaseableCharacterSwitched?.Invoke(false);
            }
        }
    }

    private void SelectCharacter()
    {
        characterSelector.SelectCharacter(currentShownCharacterIndex, saveSystemManager, () =>
                {
                    OnSelectedCharacterSwitched?.Invoke(true);
                });
    }

    private void BuyCharacter()
    {
        characterShop.BuyCharacter(currentShownCharacterSO, saveSystemManager, menuPointsTransactionManager , () =>
        {
            OnOpenCharacterSwitched?.Invoke(true);
        });
    }

}
