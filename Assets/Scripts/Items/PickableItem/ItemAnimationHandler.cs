using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAnimationHandler : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private void Awake()
    {
        if(animator == null) animator = GetComponent<Animator>();
    }
    public void OnPickedAnimationPlay()
    {
        Debug.Log("Anim");
    }
}
