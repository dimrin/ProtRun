using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MenuCharacterAnimationHolder))]
public class MenuCharacter : MonoBehaviour
{
    [SerializeField] private MenuCharacterAnimationHolder animationHolder;
    [SerializeField] private MenuCharacterMover characterMover;

    public static event Action OnAnimationActivated;
    public static event Action OnAnimationFinished;

    private void Awake()
    {
        animationHolder = GetComponent<MenuCharacterAnimationHolder>();
        characterMover = GetComponent<MenuCharacterMover>();
    }

    private void OnEnable()
    {
        MenuUIManager.OnBuyCharacter += Dance;
        MenuUIManager.OnSelectCharacter += Cheer;
        MenuUIManager.OnSelectorOpened += Wave;
        //MenuUIManager.SwitchCharacters += Dash;
        CharacterShopManager.DoAnimationOnActivated += Dash;
    }

    private void OnDisable()
    {
        MenuUIManager.OnBuyCharacter -= Dance;
        MenuUIManager.OnSelectCharacter -= Cheer;
        MenuUIManager.OnSelectorOpened -= Wave;
        //MenuUIManager.SwitchCharacters -= Dash;
        CharacterShopManager.DoAnimationOnActivated -= Dash;
    }

    public void SetFinalPosition(Transform position)
    {
        characterMover.SetFinalPosition(position);
    }

    private void Update()
    {
        characterMover.Move();
    }

    private void Wave()
    {
        animationHolder.ActivateWaveAnimation(() =>
        {
            OnAnimationActivated?.Invoke();
        });
    }

    private void Dance()
    {
        animationHolder.ActivateDanceAnimation(() =>
        {
            OnAnimationActivated?.Invoke();
        });
    }

    private void Dash()
    {
        animationHolder.ActivateDashAnimation(() =>
        {
            OnAnimationActivated?.Invoke();
        });
    }

    private void Cheer()
    {
        animationHolder.ActivateCheerAnimation(() =>
        {
            OnAnimationActivated?.Invoke();
        });
    }

    public void CallAnimationFinishedOnAnimationEvent()
    {
        OnAnimationFinished?.Invoke();
    }
}
