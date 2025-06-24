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
        Debug.Log("Sides");
    }

    public void PlaySoundOnSwipeUp() {
        PlaySound(movementAudioSource, OnUpSwipeSound);
        Debug.Log("Up");
    }

    public void PlaySoundOnCrash()
    {
        PlaySound(movementAudioSource, OnCrashSound);
        Debug.Log("Boom");
    }

    public void PlaySoundOnSwipeDown()
    {
        PlaySound(movementAudioSource, OnSwipeDownSound);
        Debug.Log("Down");
    }

    public void PlaySoundOnPickValue()
    {
        PlaySound(pickingAudioSource, OnValueItemSound);
        Debug.Log("Money");
    }

    public void PlaySoundOnPickBuf()
    {
        PlaySound(pickingAudioSource, OnBufItemSound);
        Debug.Log("Buf");
    }

    public void PlaySoundOnDestroyObstacle()
    {
        PlaySound(pickingAudioSource, OnDestroyObstacleSound);
        Debug.Log("Obstacle");
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
