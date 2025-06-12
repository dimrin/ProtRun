using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.NetworkInformation;
using Newtonsoft.Json;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    [SerializeField] private string saveFileName;

    [SerializeField] private int points = 0;

    private int currentCharacterIndex = 0;

    private bool isVibrating = true;
    private float musicVolumeValue = 0f;

    private int languageIndex = -1;

    private List<int> openCharactersList = new List<int>();

    public static SaveManager Instance;

    private bool isLoaded = false;

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

    public void Save()
    {
        PlayerData data = new PlayerData();

        data.points = points;
        data.musicVolumeValue = musicVolumeValue;
        data.languageIndex = languageIndex;
        data.openCharactersList = openCharactersList;
        data.currentCharacterIndex = currentCharacterIndex;
        data.isVibrating = isVibrating;

        string jsonData = JsonConvert.SerializeObject(data);

        string path = Path.Combine(Application.persistentDataPath, saveFileName);

        File.WriteAllText(path, jsonData);

        Debug.Log(path);

        Debug.Log("Saved");
    }

    public void Load() {
        string path = Path.Combine(Application.persistentDataPath, saveFileName);

        if (File.Exists(path))
        {
            string dataFromJson = File.ReadAllText(path);

            Debug.Log("Data " + dataFromJson);

            PlayerData data = JsonConvert.DeserializeObject<PlayerData>(dataFromJson);

            currentCharacterIndex = data.currentCharacterIndex;
            openCharactersList = data.openCharactersList;
            points = data.points;
            isVibrating = data.isVibrating;
            musicVolumeValue = data.musicVolumeValue;
            languageIndex = data.languageIndex;
            isLoaded = true;

        }
        else
        {
            isLoaded = true;
            openCharactersList.Add(currentCharacterIndex);
            Debug.Log("Has no local Data");

        }
    }
}


[Serializable]
class PlayerData {

    public int points;

    public int currentCharacterIndex;
    public List<int> openCharactersList;

    public bool isVibrating;
    public float musicVolumeValue;
    public int languageIndex;

}
