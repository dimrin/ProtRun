using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioUIManager : MonoBehaviour {
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private AudioClip onClickSound;

    private void Awake()
    {
        OnAwakeSetUp();
    }

    private void OnAwakeSetUp()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = false;
        if (audioSource.clip == null)
        {
            audioSource.clip = onClickSound;
        }
    }


    public void PlayOnClickSound()
    {
        audioSource.Play();
    }

}
