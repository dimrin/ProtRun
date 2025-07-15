using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MenuCharacterAnimationHolder : MonoBehaviour {
    [SerializeField] private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void ActivateWaveAnimation()
    {
        animator.SetTrigger("Wave");
    }

}
