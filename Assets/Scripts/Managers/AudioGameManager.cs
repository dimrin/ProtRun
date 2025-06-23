using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class AudioGameManager : MonoBehaviour {
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private AudioClip OnRunSound;
    [SerializeField] private AudioClip OnFInishSound;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
    }

    private void OnEnable()
    {
        GameSessionManager.OnGameStarted += PlayOnStart;
        GameSessionManager.OnGameRun += ResumeOnRun;
        GameSessionManager.OnGamePaused += PauseOnRun;
        GameSessionManager.OnGameFinished += PlayOnFinish;
        GameSessionManager.OnRevived += PlayOnStart;
    }

    private void OnDisable()
    {
        GameSessionManager.OnGameStarted -= PlayOnStart;
        GameSessionManager.OnGameRun -= ResumeOnRun;
        GameSessionManager.OnGamePaused -= PauseOnRun;
        GameSessionManager.OnGameFinished -= PlayOnFinish;
        GameSessionManager.OnRevived -= PlayOnStart;
    }

    private void PlayOnStart()
    {
        audioSource.clip = OnRunSound;
        audioSource.Play();
    }

    private void PauseOnRun()
    {
        audioSource.Pause();
    }

    private void ResumeOnRun()
    {
        audioSource.UnPause();
    }

    private void PlayOnFinish()
    {
        audioSource.clip = OnFInishSound;
        audioSource.Play();
    }


}
