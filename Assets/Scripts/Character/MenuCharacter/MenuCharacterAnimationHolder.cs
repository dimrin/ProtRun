using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MenuCharacterAnimationHolder : MonoBehaviour {
    [SerializeField] private Animator animator;



    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void ActivateWaveAnimation(Action OnAnimationActivated)
    {
        animator.SetTrigger("Wave");

        OnAnimationActivated?.Invoke();
    }

    public void ActivateDanceAnimation(Action OnAnimationActivated)
    {
        animator.SetTrigger("Dance");

        OnAnimationActivated?.Invoke();
    }

    public void ActivateDashAnimation(Action OnAnimationActivated)
    {
        animator.SetTrigger("Dash");

        OnAnimationActivated?.Invoke();
    }

    public void ActivateCheerAnimation(Action OnAnimationActivated)
    {
        animator.SetTrigger("Cheer");

        OnAnimationActivated?.Invoke();
    }
}
