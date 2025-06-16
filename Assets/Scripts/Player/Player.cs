using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour {

    [SerializeField] private PlayerLaneMovement movement;
    [SerializeField] private PlayerJumpMovement jumpMovement;
    [SerializeField] private PlayerItemPicker itemPicker;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private PlayerBuffManager playerBuffManager;
    //[SerializeField] private PlayerSwipeInput input;

    public static event Action<int> OnPickedPoint;
    public static event Action OnPlayerCrushed;

    private void OnEnable()
    {
        PlayerSwipeInput.SwipeToLeft += movement.MoveLeft;
        PlayerSwipeInput.SwipeToRight += movement.MoveRight;
        PlayerSwipeInput.SwipeToUp += jumpMovement.Jump;
    }


    private void OnDisable()
    {
        PlayerSwipeInput.SwipeToLeft -= movement.MoveLeft;
        PlayerSwipeInput.SwipeToRight -= movement.MoveRight;
        PlayerSwipeInput.SwipeToUp -= jumpMovement.Jump;
    }

    private void Awake()
    {
        movement = GetComponent<PlayerLaneMovement>();
        jumpMovement = GetComponent<PlayerJumpMovement>();
        itemPicker = GetComponent<PlayerItemPicker>();
        playerHealth = GetComponent<PlayerHealth>();
        playerBuffManager = GetComponent<PlayerBuffManager>();
        //input = GetComponent<PlayerSwipeInput>();
    }

    private void Update()
    {
        movement.IncreaseSpeed();
        movement.UpdateTargetPosition();
        movement.Move();
        jumpMovement.ApplyGravity();
        jumpMovement.MoveVertical();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        itemPicker.PickItem(hit.gameObject, (itemType, itemValue) =>
        {
            /*
            switch (itemType)
            {
                default:
                    Debug.LogWarning("No Type like that"); break;
                case ItemType.Value:
                    OnPickedPoint(itemValue); break;
                case ItemType.Shield:
                    Debug.Log("Shield");
                    int shieldTime = itemValue;
                    playerHealth.MakeUnhittable(shieldTime);
                    break;
                case ItemType.Ultimate:
                    Debug.Log("Ultimate");
                    int ultimateTime = itemValue;
                    playerHealth.MakeUnhittable(ultimateTime);
                    movement.ActivateSpeedBuff(ultimateTime);
                    break;
            }
            */
            switch (itemType)
            {
                case ItemType.Value:
                    OnPickedPoint?.Invoke(itemValue);
                    break;
                case ItemType.Shield:
                case ItemType.Ultimate:
                case ItemType.Magnet:
                    playerBuffManager.ApplyBuff(itemType, itemValue);
                    break;
                default:
                    Debug.LogWarning("Unhandled item type: " + itemType);
                    break;
            }

        });

        playerHealth.GetHit(hit.gameObject, () =>
        {
            Debug.Log("Hitted");
            OnPlayerCrushed?.Invoke();
        });
    }



}
