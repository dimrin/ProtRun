using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class CharacterSelector : MonoBehaviour
{
    [SerializeField] private List<int> openCharacters = new List<int>();

    public void SetOpenCharactersList(List<int> openCharactersList)
    {
        openCharacters = openCharactersList;
    }

    public bool IsCurrentCharacterOpened(int index)
    {
        return openCharacters.Contains(index);
    }

    public bool IsCurrentCharacterSelected(int shownCharacterIndex, int currentCharacterIndex)
    {
        return shownCharacterIndex == currentCharacterIndex;
    }

    public void SelectCharacter(int characterIndex, SaveSystemManager saveSystemManager ,Action OnCharacterSelected)
    {
        saveSystemManager.SetCurrentCharacterIndex(characterIndex);
        saveSystemManager.Save();
        OnCharacterSelected?.Invoke();
    }
}
