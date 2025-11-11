using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.NetworkInformation;
using Newtonsoft.Json;
using UnityEngine;

public class SaveSystemManager : MonoBehaviour {
    [SerializeField] private string saveFileName;

    [SerializeField] private static int points = 0;

    private int currentCharacterIndex = 0;

    [SerializeField] private static bool isVibrating = true;
    [SerializeField] private static float musicVolumeValue = 0f;

    private int languageIndex = -1;

    [SerializeField] private List<int> openCharactersList = new List<int>();

    public static SaveSystemManager Instance;

    private bool isLoaded = false;

    private float test1 = 0f;
    private string test2 = "";
    private Dictionary<string, float> test3 = new Dictionary<string, float>();
    private List<String> test4 = new List<string>();
    private bool test5 = false;

    private void Awake()
    {
        DoNotDestroy();
    }


    private void DoNotDestroy()
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

    private void SetDefaultValues()
    {
        SetCharacterToList(0);
    }

    public void AddPoints(int pointsToAdd)
    {
        points += pointsToAdd;
    }

    public void RemovePoints(int pointsToRemove)
    {
        points -= pointsToRemove;
    }

    public int GetPoints()
    {
        return points;
    }

    public bool IsLoaded() { return isLoaded; }

    public bool IsVibrating() { return isVibrating; }

    public float GetVolume() { return musicVolumeValue; } 

    public void SetVibrationState(bool state)
    {
        isVibrating = state;
    }

    public void SetVolume(float volume)
    {
        musicVolumeValue = volume;
    }

    public int GetCurrentCharacterIndex()
    {
        return currentCharacterIndex;
    }


    public List<int> GetOpenCharactersList()
    {
        return openCharactersList;
    }

    public void SetCharacterToList(int index)
    {
        if (!openCharactersList.Contains(index))
        {
            openCharactersList.Add(index);
        }

    }

    public void SetCurrentCharacterIndex(int index)
    {
        currentCharacterIndex = index;
    }

    public void Save()
    {
        PlayerData data = new PlayerData();

        data.points = points;
        data.musicVolumeValue = musicVolumeValue;
        data.languageIndex = languageIndex;
        data.openCharactersList = openCharactersList;
        Debug.Log(openCharactersList.Count + " Open List 1");
        data.currentCharacterIndex = currentCharacterIndex;
        data.isVibrating = isVibrating;

        string jsonData = JsonConvert.SerializeObject(data);

        string path = Path.Combine(Application.persistentDataPath, saveFileName);

        File.WriteAllText(path, jsonData);

        Debug.Log(path);

        Debug.Log("Saved");
    }

    public void Load()
    {
        string path = Path.Combine(Application.persistentDataPath, saveFileName);

        if (File.Exists(path))
        {
            string dataFromJson = File.ReadAllText(path);

            Debug.Log("Data " + dataFromJson);

            PlayerData data = JsonConvert.DeserializeObject<PlayerData>(dataFromJson);

            currentCharacterIndex = data.currentCharacterIndex;
            openCharactersList = data.openCharactersList;
            Debug.Log(openCharactersList.Count + " Open List 2");
            points = data.points;
            isVibrating = data.isVibrating;
            musicVolumeValue = data.musicVolumeValue;
            languageIndex = data.languageIndex;

            test1 = data.test1;
            Debug.Log($"data.test1 = {data.test1}");
            test2 = data.test2;
            Debug.Log($"data.test2 = {data.test2}");
            test3 = data.test3;
            Debug.Log($"data.test3 = {data.test3}");
            test4 = data.test4;
            Debug.Log($"data.test4 = {data.test4}");
            test5 = data.test5;
            Debug.Log($"data.test5 = {data.test5}");

            try
            {
                test1 = data.test1;
            } catch (Exception e)
            {
                Debug.LogError($"data.test1 = {e}");
            }

            try
            {
                test2 = data.test2;
            }
            catch (Exception e)
            {
                Debug.LogError($"data.test2 = {e}");
            }
            try
            {
                test3 = data.test3;
            }
            catch (Exception e)
            {
                Debug.LogError($"data.test3 = {e}");
            }
            try
            {
                test4 = data.test4;
            }
            catch (Exception e)
            {
                Debug.LogError($"data.test4 = {e}");
            }
            try
            {
                test5 = data.test5;
            }
            catch (Exception e)
            {
                Debug.LogError($"data.test5 = {e}");
            }

            isLoaded = true;

        }
        else
        {
            isLoaded = true;
            SetDefaultValues();
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
    public float test1;
    public string test2;
    public Dictionary<string, float> test3;
    public List<String> test4;
    public bool test5;
}
