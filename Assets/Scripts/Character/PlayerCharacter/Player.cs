using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.UIElements;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour {

    [SerializeField] private PlayerLaneMovement playerLaneMovement;
    [SerializeField] private PlayerVerticalMovement playerVerticalMovement;
    [SerializeField] private PlayerItemPicker itemPicker;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private PlayerBuffManager playerBuffManager;
    [SerializeField] private PlayerSoundsManager playerSoundsManager;
    [SerializeField] private PlayerAnimationHandler playerAnimationHandler;
    [SerializeField] private PlayerEffectsManager playerEffectsManager;

    [SerializeField] private int onReviveBuffTime = 5;

    public static event Action<int> OnPickedPoint;
    public static event Action OnPlayerCrushed;
    public static event Action<ItemType, int> OnBuffApplied;

    private bool _canMove = false;
    private bool _isPlayedCrashed = false;

    private void OnEnable()
    {
        PlayerSwipeInput.SwipeToLeft += playerSoundsManager.PlaySoundOnLeftRightSwipe;
        PlayerSwipeInput.SwipeToLeft += playerLaneMovement.MoveLeft;
        PlayerSwipeInput.SwipeToRight += playerSoundsManager.PlaySoundOnLeftRightSwipe;
        PlayerSwipeInput.SwipeToRight += playerLaneMovement.MoveRight;
        PlayerSwipeInput.SwipeToUp += Jump;
        PlayerSwipeInput.SwipeToDown += PushDown;
        GameSessionManager.OnGameRun += AllowMove;
        GameSessionManager.OnGamePaused += StopMove;
        GameSessionManager.OnGameFinished += StopMove;
        GameSessionManager.OnRevived += Revive;
    }


    private void OnDisable()
    {
        PlayerSwipeInput.SwipeToLeft -= playerSoundsManager.PlaySoundOnLeftRightSwipe;
        PlayerSwipeInput.SwipeToLeft -= playerLaneMovement.MoveLeft;
        PlayerSwipeInput.SwipeToRight -= playerSoundsManager.PlaySoundOnLeftRightSwipe;
        PlayerSwipeInput.SwipeToRight -= playerLaneMovement.MoveRight;
        PlayerSwipeInput.SwipeToUp -= Jump;
        PlayerSwipeInput.SwipeToDown -= PushDown;
        GameSessionManager.OnGameRun -= AllowMove;
        GameSessionManager.OnGamePaused -= StopMove;
        GameSessionManager.OnGameFinished -= StopMove;
        GameSessionManager.OnRevived -= Revive;
    }

    private void Awake()
    {
        playerLaneMovement = GetComponent<PlayerLaneMovement>();
        playerVerticalMovement = GetComponent<PlayerVerticalMovement>();
        itemPicker = GetComponent<PlayerItemPicker>();
        playerHealth = GetComponent<PlayerHealth>();
        playerBuffManager = GetComponent<PlayerBuffManager>();
        playerSoundsManager = GetComponent<PlayerSoundsManager>();
        playerEffectsManager = GetComponent<PlayerEffectsManager>();
        playerBuffManager.SetComponentsOnAwake(playerHealth, playerLaneMovement, transform);
    }

    private void Update()
    {
        if (!_canMove) return;
        playerLaneMovement.IncreaseSpeed();
        playerLaneMovement.UpdateTargetPosition();
        playerLaneMovement.Move();
        playerVerticalMovement.ApplyGravity();
        playerVerticalMovement.MoveUp();
        if(!playerVerticalMovement.IsJumping && !_isPlayedCrashed)
        {
            playerAnimationHandler.DeactivateJumpAnimation();
            playerAnimationHandler.ActivateRunAnimation();
        }
        playerBuffManager.UpdateBuffsStates(() =>
        {
            //playerAnimationHandler.DeactivateSpinningAnimation();
        });
    }

    private void StopMove()
    {
        _canMove = false;
    }

    private void AllowMove()
    {
        _canMove = true;
        playerAnimationHandler.ActivateRunAnimation();
    }

    private void Jump()
    {

        playerVerticalMovement.Jump(() =>
        {
            playerAnimationHandler.DeactivateRunAnimation();
            playerAnimationHandler.ActivateJUmpAnimation();
            playerSoundsManager.PlaySoundOnSwipeUp();
        });
    }

    private void PushDown()
    {  
        playerVerticalMovement.PushDown(()=>
        {
            playerAnimationHandler.ActivateRunAnimation();
            playerSoundsManager.PlaySoundOnSwipeDown();
        });
    }

    private void Revive()
    {
        AllowMove();
        playerBuffManager.ApplyBuff(ItemType.Speed, onReviveBuffTime);
        OnBuffApplied?.Invoke(ItemType.Speed, onReviveBuffTime);
        playerAnimationHandler.ActivateRunAnimation();
        _isPlayedCrashed = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        itemPicker.PickItem(other.gameObject, (itemType, itemValue) =>
        {
            switch (itemType)
            {
                case ItemType.Value:
                    OnPickedPoint?.Invoke(itemValue);
                    playerSoundsManager.PlaySoundOnPickValue();
                    break;
                case ItemType.Shield:
                    playerBuffManager.ApplyBuff(itemType, itemValue);
                    playerAnimationHandler.ActivateSpinningAnimation(itemValue);
                    OnBuffApplied?.Invoke(itemType, itemValue);
                    playerSoundsManager.PlaySoundOnPickBuf();
                    break;
                case ItemType.Speed:
                    playerBuffManager.ApplyBuff(itemType, itemValue);
                    playerAnimationHandler.ActivateSpinningAnimation(itemValue);
                    OnBuffApplied?.Invoke(itemType, itemValue);
                    playerSoundsManager.PlaySoundOnPickBuf();
                    break;
                case ItemType.Magnet:
                    playerBuffManager.ApplyBuff(itemType, itemValue);
                    playerEffectsManager.ActivateMangetObject(itemValue);
                    OnBuffApplied?.Invoke(itemType, itemValue);
                    playerSoundsManager.PlaySoundOnPickBuf();
                    break;
                default:
                    Debug.LogWarning("Unhandled item type: " + itemType);
                    break;
            }

        });
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        playerHealth.GetHit(hit.gameObject, () =>
        {
            playerSoundsManager.PlaySoundOnDestroyObstacle();
        },() =>
        {
            Debug.Log("Hitted");
            playerAnimationHandler.DeactivateRunAnimation();
            playerAnimationHandler.ActivateDefeatAnimation();
            OnPlayerCrushed?.Invoke();
            _isPlayedCrashed = true;
            playerSoundsManager.PlaySoundOnCrash();
        });
    }



}
