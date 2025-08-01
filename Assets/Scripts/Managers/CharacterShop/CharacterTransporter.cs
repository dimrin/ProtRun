using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTransporter : MonoBehaviour
{
    [SerializeField] private CharacterSO selectedPlayer;

    private static CharacterTransporter Instance;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }


    public void SetCharacterSOToTransport(CharacterSO characterSO)
    {
        selectedPlayer = characterSO;
    }

    public CharacterSO GetTransportedCharacter()
    {
        return  selectedPlayer;
    }
}
