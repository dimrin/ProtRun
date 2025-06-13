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
            switch (itemType) {
                default:
                    Debug.LogWarning("No Type like that"); break;
                case ItemType.Value:
                    OnPickedPoint(itemValue); break;
            }
        });

        playerHealth.GetHit(hit.gameObject, () =>
        {
            Debug.Log("Hitted");
            OnPlayerCrushed?.Invoke();
        });
    }



}
