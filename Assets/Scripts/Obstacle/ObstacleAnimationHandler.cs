using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ObstacleAnimationHandler : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private void Awake()
    {
        if(animator == null) animator = GetComponent<Animator>();
    }

    public void OnHitAnimate()
    {

    }
}
