using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MenuCharacterAnimationHolder))]
public class MenuCharacter : MonoBehaviour
{
    [SerializeField] private MenuCharacterAnimationHolder animationHolder;

    private void Awake()
    {
        animationHolder = GetComponent<MenuCharacterAnimationHolder>();
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    private void Wave()
    {
        animationHolder.ActivateWaveAnimation();
    }
}
