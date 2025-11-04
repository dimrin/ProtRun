using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class GameUIAnimationHandler : MonoBehaviour
{
    [SerializeField] private Animator animator;


    private void Awake()
    {
        if (animator == null) animator = GetComponent<Animator>();
    }

    public void ActivateCountDownAnimation()
    {
        animator.SetTrigger("StartCountDown");
    }
    
}
