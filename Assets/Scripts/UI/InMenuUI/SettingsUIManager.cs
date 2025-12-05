using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class SettingsUIManager : MonoBehaviour
{
    [SerializeField] private Slider volumeSLider;

    [SerializeField] private Toggle vibrationToggle;

    [SerializeField] private SliderPointerEvent volumeSliderPointerEvents;

    [SerializeField] private Button loginGoogleButton;

    public event Action<bool> OnVibrationChanged;

    public event Action OnFinalVolumeValue;
    public event Action<float> OnVolumeChanged;

    public event Action CloseOnClick;
    public event Action OnLoginGoogleClicked;

    private void OnEnable()
    {
        vibrationToggle.onValueChanged.AddListener(OnVibrationStateChanged);
        volumeSliderPointerEvents.onPointerUp += OnFinalVolmeValueSet;
    }

    private void OnDisable()
    {
        vibrationToggle.onValueChanged.RemoveListener(OnVibrationStateChanged);
        volumeSliderPointerEvents.onPointerUp -= OnFinalVolmeValueSet;
    }

    public void OnVibrationStateChanged(bool state)
    {
        OnVibrationChanged?.Invoke(state);
    }

    public void ChangeVolumeValue(float value)
    {
        OnVolumeChanged?.Invoke(value);
    }

    private void OnFinalVolmeValueSet()
    {
        OnFinalVolumeValue?.Invoke();
    }

    public void OpenUI()
    {
        gameObject.SetActive(true);
    }

    public void CloseUI()
    {
        gameObject.SetActive(false);
    }

    public void OpenUI(Action OnUIOpened)
    {
        gameObject.SetActive(true);

        OnUIOpened?.Invoke();
    }

    public void CloseUI(Action OnUIClosed)
    {
        gameObject.SetActive(false);
        OnUIClosed?.Invoke();
    }

    public void CloseUIOnClick()
    {
        CloseOnClick?.Invoke();
    }

    public void SetVolumeUIValue(float volume)
    {
        volumeSLider.value = volume;
    }

    public void SetVibrationUIValue(bool state)
    {
        vibrationToggle.isOn = state;
    }

    public void LoginGoogleOnClick()
    {
        OnLoginGoogleClicked?.Invoke();
    }

    public void SetLoginGoogleButtonUnclickable()
    {
        loginGoogleButton.interactable = false;
    }
}
