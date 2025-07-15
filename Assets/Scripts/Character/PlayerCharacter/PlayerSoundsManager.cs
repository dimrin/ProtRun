using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerSoundsManager : MonoBehaviour
{
    [SerializeField] private AudioSource movementAudioSource;
    [SerializeField] private AudioSource pickingAudioSource;

    [Header("Swipe Sounds")]
    [SerializeField] private AudioClip OnSidesSwipeSound;
    [SerializeField] private AudioClip OnUpSwipeSound;
    [SerializeField] private AudioClip OnSwipeDownSound;
    [SerializeField] private AudioClip OnCrashSound;

    [Header("Item Sounds")]
    [SerializeField] private AudioClip OnValueItemSound;
    [SerializeField] private AudioClip OnBufItemSound;
    [SerializeField] private AudioClip OnDestroyObstacleSound;

    private void Awake()
    {
        if (movementAudioSource == null) Debug.LogWarning("movementAudioSource is null");
        if (pickingAudioSource == null) Debug.LogWarning("pickingAudioSource is null");
        movementAudioSource.loop = false;
        pickingAudioSource.loop = false;
    }

    public void PlaySoundOnLeftRightSwipe()
    {
        PlaySound(movementAudioSource, OnSidesSwipeSound);
    }

    public void PlaySoundOnSwipeUp() {
        PlaySound(movementAudioSource, OnUpSwipeSound);
    }

    public void PlaySoundOnCrash()
    {
        PlaySound(movementAudioSource, OnCrashSound);
    }

    public void PlaySoundOnSwipeDown()
    {
        PlaySound(movementAudioSource, OnSwipeDownSound);
    }

    public void PlaySoundOnPickValue()
    {
        PlaySound(pickingAudioSource, OnValueItemSound);
    }

    public void PlaySoundOnPickBuf()
    {
        PlaySound(pickingAudioSource, OnBufItemSound);
    }

    public void PlaySoundOnDestroyObstacle()
    {
        PlaySound(pickingAudioSource, OnDestroyObstacleSound);
    }

    private void PlaySound(AudioSource audioSource, AudioClip audioClip)
    {
        if(audioSource.isPlaying)
        {
            audioSource.Stop();
        }
        audioSource.clip = audioClip;
        audioSource.Play();
    }
}
