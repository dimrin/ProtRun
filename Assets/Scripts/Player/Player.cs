using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour {

    [SerializeField] private PlayerLaneMovement playerLaneMovement;
    [SerializeField] private PlayerVerticalMovement playerVerticalMovement;
    [SerializeField] private PlayerItemPicker itemPicker;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private PlayerBuffManager playerBuffManager;

    public static event Action<int> OnPickedPoint;
    public static event Action OnPlayerCrushed;

    private bool canMove = false;

    private void OnEnable()
    {
        PlayerSwipeInput.SwipeToLeft += playerLaneMovement.MoveLeft;
        PlayerSwipeInput.SwipeToRight += playerLaneMovement.MoveRight;
        PlayerSwipeInput.SwipeToUp += playerVerticalMovement.Jump;
        PlayerSwipeInput.SwipeToDown += playerVerticalMovement.PushDown;
        GameSessionManager.OnGameRun += AllowMove;
        GameSessionManager.OnGamePaused += StopMove;
        GameSessionManager.OnGameFinished += StopMove;
    }


    private void OnDisable()
    {
        PlayerSwipeInput.SwipeToLeft -= playerLaneMovement.MoveLeft;
        PlayerSwipeInput.SwipeToRight -= playerLaneMovement.MoveRight;
        PlayerSwipeInput.SwipeToUp -= playerVerticalMovement.Jump;
        PlayerSwipeInput.SwipeToDown -= playerVerticalMovement.PushDown;
        GameSessionManager.OnGameRun -= AllowMove;
        GameSessionManager.OnGamePaused -= StopMove;
        GameSessionManager.OnGameFinished -= StopMove;
    }

    private void Awake()
    {
        playerLaneMovement = GetComponent<PlayerLaneMovement>();
        playerVerticalMovement = GetComponent<PlayerVerticalMovement>();
        itemPicker = GetComponent<PlayerItemPicker>();
        playerHealth = GetComponent<PlayerHealth>();
        playerBuffManager = GetComponent<PlayerBuffManager>();
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

    private void OnTriggerEnter(Collider other)
    {
        itemPicker.PickItem(other.gameObject, (itemType, itemValue) =>
        {
            switch (itemType)
            {
                case ItemType.Value:
                    OnPickedPoint?.Invoke(itemValue);
                    break;
                case ItemType.Shield:
                case ItemType.Speed:
                case ItemType.Magnet:
                    playerBuffManager.ApplyBuff(itemType, itemValue);
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
            Debug.Log("Hitted");
            OnPlayerCrushed?.Invoke();
        });
    }



}
