using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectorManaher : MonoBehaviour {
    [SerializeField] private List<CharacterSO> charactersSO = new List<CharacterSO>();

    [SerializeField] private PlayerSpawnInMenuManager spawnInMenuManager;

    [SerializeField] private SaveSystemManager saveSystemManager;

    [SerializeField] private List<Dictionary<int, GameObject>> spawnedList = new List<Dictionary<int, GameObject>>();

    public static event Action DoAnimationOnActivated;

    private int currentCharacterIndex = -1;

    private GameObject currentMenuCharacter;

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


        SpawnCharacters();
    }

    private void OnEnable()
    {
        SaveLoadManager.DataLoaded += ActivateSavedCharacter;
        MenuUIManager.SwitchCharacters += SwitchCharacters;
        MenuUIManager.OnSelectorClosed += ActivateSavedCharacter;
    }

    private void OnDisable()
    {
        SaveLoadManager.DataLoaded -= ActivateSavedCharacter;
        MenuUIManager.SwitchCharacters -= SwitchCharacters;
        MenuUIManager.OnSelectorClosed -= ActivateSavedCharacter;
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
        if (currentCharacterIndex != saveSystemManager.GetCurrentCharacterIndex())
        {
            currentCharacterIndex = saveSystemManager.GetCurrentCharacterIndex();

            DeactivateCurrentCharacter();

            ActivateCharacter(currentCharacterIndex);
        }
    }


    private void ActivateCharacter(int index)
    {
        foreach (var characterPair in spawnedList)
        {
            if (characterPair.ContainsKey(index))
            {
                characterPair[index].SetActive(true);
                currentMenuCharacter = characterPair[index];
                DoAnimationOnActivated?.Invoke();
            }
        }
    }

    private void DeactivateCurrentCharacter()
    {
        if (currentMenuCharacter != null)
        {
            currentMenuCharacter.SetActive(false);
        }

    }

    private void SwitchCharacters(int index)
    {
        DeactivateCurrentCharacter();


        currentCharacterIndex += index;


        if (currentCharacterIndex > spawnedList.Count - 1)
        {
            currentCharacterIndex = 0;
        }
        else if (currentCharacterIndex < 0)
        {
            currentCharacterIndex = spawnedList.Count - 1;
        }


        ActivateCharacter(currentCharacterIndex);
    }

}
