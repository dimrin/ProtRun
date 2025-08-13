using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class LoadingUIAnimationHandler : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private void Awake()
    {
        if(animator == null) animator = GetComponent<Animator>();
    }

    public void ActivateCurrentLevelLoadAnimation()
    {
        animator.SetTrigger("LoadCurrentLevel");
    }

    public void ActivateNewLevelLoadAnimation()
    {
        animator.SetTrigger("LoadNewLevel");
    }
}
