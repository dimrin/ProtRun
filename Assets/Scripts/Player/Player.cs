using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour {

    [SerializeField] private PlayerLaneMovement playerLaneMovement;
    [SerializeField] private PlayerJumpMovement jumpMovement;
    [SerializeField] private PlayerItemPicker itemPicker;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private PlayerBuffManager playerBuffManager;
    //[SerializeField] private PlayerSwipeInput input;

    public static event Action<int> OnPickedPoint;
    public static event Action OnPlayerCrushed;

    private void OnEnable()
    {
        PlayerSwipeInput.SwipeToLeft += playerLaneMovement.MoveLeft;
        PlayerSwipeInput.SwipeToRight += playerLaneMovement.MoveRight;
        PlayerSwipeInput.SwipeToUp += jumpMovement.Jump;
    }


    private void OnDisable()
    {
        PlayerSwipeInput.SwipeToLeft -= playerLaneMovement.MoveLeft;
        PlayerSwipeInput.SwipeToRight -= playerLaneMovement.MoveRight;
        PlayerSwipeInput.SwipeToUp -= jumpMovement.Jump;
    }

    private void Awake()
    {
        playerLaneMovement = GetComponent<PlayerLaneMovement>();
        jumpMovement = GetComponent<PlayerJumpMovement>();
        itemPicker = GetComponent<PlayerItemPicker>();
        playerHealth = GetComponent<PlayerHealth>();
        playerBuffManager = GetComponent<PlayerBuffManager>();
        playerBuffManager.SetComponentsOnAwake(playerHealth, playerLaneMovement, transform);
        //input = GetComponent<PlayerSwipeInput>();
    }

    private void Update()
    {
        playerLaneMovement.IncreaseSpeed();
        playerLaneMovement.UpdateTargetPosition();
        playerLaneMovement.Move();
        jumpMovement.ApplyGravity();
        jumpMovement.MoveVertical();
        playerBuffManager.UpdateBuffsStates();
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
