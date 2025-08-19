using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterScreenUIAnimationHandler : MonoBehaviour
{
    [SerializeField] private Animator animator;

    //[SerializeField] private GameObject animatedObject;

    private void Awake()
    {
       if(animator == null)animator = GetComponent<Animator>();
    }

    public void StartLoadingAnimation()
    {
        //animatedObject.SetActive(true);
        //animator.SetBool("isEnterLoading", true);
        animator.SetTrigger("isEnterLoading");
    }

    public void StopLoadingAnimation(Action OnAnimationEnded)
    {
        //animator.SetBool("isEnterLoading", false);
        //animatedObject.SetActive(false);
        OnAnimationEnded?.Invoke();
    }

    public void StartEnterAnimation()
    {
        //animator.SetBool("isEnterLoaded", true);
        animator.SetTrigger("isEnterLoaded");
    }

    public void StopEnterAnimation(Action OnAnimationEnded) {
        //animator.SetBool("isEnterLoaded", false);
        OnAnimationEnded?.Invoke();
    }

}
