using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterTransporter : MonoBehaviour
{
    [SerializeField] private CharacterSO selectedPlayer;

    public static CharacterTransporter Instance;

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

        Debug.Log(SceneManager.GetActiveScene().name);
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
