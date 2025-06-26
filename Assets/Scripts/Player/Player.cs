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

    [SerializeField] private int onReviveBuffTime = 5;

    public static event Action<int> OnPickedPoint;
    public static event Action OnPlayerCrushed;
    public static event Action<ItemType, int> OnBuffApplied;

    private bool canMove = false;

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
        playerBuffManager.SetComponentsOnAwake(playerHealth, playerLaneMovement, transform);
    }

    private void Update()
    {
        if (!canMove) return;
        playerLaneMovement.IncreaseSpeed();
        playerLaneMovement.UpdateTargetPosition();
        playerLaneMovement.Move();
        playerVerticalMovement.ApplyGravity();
        playerVerticalMovement.MoveUp();
        playerBuffManager.UpdateBuffsStates();
    }

    private void StopMove()
    {
        canMove = false;
    }

    private void AllowMove()
    {
        canMove = true;
    }

    private void Jump()
    {
        playerVerticalMovement.Jump(() =>
        {
            playerSoundsManager.PlaySoundOnSwipeUp();
        });
    }

    private void PushDown()
    {
        playerVerticalMovement.PushDown(()=>
        {
            playerSoundsManager.PlaySoundOnSwipeDown();
        });
    }

    private void Revive()
    {
        AllowMove();
        playerBuffManager.ApplyBuff(ItemType.Speed, onReviveBuffTime);
        OnBuffApplied?.Invoke(ItemType.Speed, onReviveBuffTime);
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
                case ItemType.Speed:
                case ItemType.Magnet:
                    playerBuffManager.ApplyBuff(itemType, itemValue);
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
            OnPlayerCrushed?.Invoke();
            playerSoundsManager.PlaySoundOnCrash();
        });
    }



}
