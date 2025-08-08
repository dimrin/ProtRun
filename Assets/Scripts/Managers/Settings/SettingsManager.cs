using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private AudioMixer gameAudioMixer;


    [SerializeField] private SaveSystemManager saveSystemManager;

    [SerializeField] private VibrationManager vibrationManager;

    public static event Action<float> VolumeValueOnDataLoaded;
    public static event Action<bool> VibrationValueOnDataLoaded;

    private void Awake()
    {
        if(saveSystemManager == null) saveSystemManager = FindAnyObjectByType<SaveSystemManager>();
        if(vibrationManager == null) vibrationManager = FindAnyObjectByType<VibrationManager>();
    }



    private void OnEnable()
    {
        SaveFileLoaderManager.DataLoaded += SetSavedValues;
        MenuUIManager.OnVibrationSet += SetVibration;
        MenuUIManager.OnVolumeSet += SetVolumeValue;
        MenuUIManager.OnFinalVolumeSet += SaveValues;
    }

    private void OnDisable()
    {
        SaveFileLoaderManager.DataLoaded -= SetSavedValues;
        MenuUIManager.OnVibrationSet -= SetVibration;
        MenuUIManager.OnVolumeSet -= SetVolumeValue;
        MenuUIManager.OnFinalVolumeSet -= SaveValues;
    }


    private void SetSavedValues()
    {

        float volumeToSet = saveSystemManager.GetVolume();
        bool stateToSet = saveSystemManager.IsVibrating();

        VolumeValueOnDataLoaded?.Invoke(volumeToSet);
        SetVolumeWithoutSave(volumeToSet);
        vibrationManager.SetVibration(stateToSet);
        VibrationValueOnDataLoaded?.Invoke(stateToSet);

    }

    private void SetVolumeValue(float volume)
    {
        //gameAudioMixer.SetFloat("volume", volume);
        SetVolumeWithoutSave(volume);
        saveSystemManager.SetVolume(volume);
    }

    private void SetVolumeWithoutSave(float volumeToSet)
    {
        gameAudioMixer.SetFloat("volume", volumeToSet);
        float mixerVolume = 0f;
        gameAudioMixer.GetFloat("volume", out mixerVolume);
        Debug.Log($"Volume {volumeToSet} set, and get {mixerVolume}");
    }

    private void SetVibration(bool state)
    {
        SetVibrationValue(state, () =>
        {
            SaveValues();
        });
    }

    private void SetVibrationValue(bool isVibrating, Action OnValueSet)
    {
        vibrationManager.SetVibration(isVibrating);
        saveSystemManager.SetVibrationState(isVibrating);
        
        OnValueSet?.Invoke();
    }

    private void SaveValues()
    {
        saveSystemManager.Save();
    }

}
