using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultCharacterHandler : MonoBehaviour
{
    [SerializeField] private CharacterSO defaultCharacter;


    public CharacterSO GetDefaultCharacter()
    {
        return defaultCharacter;
    }
}
