using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField] private PlayerLaneMovement movement;
    [SerializeField] private PlayerJumpMovement jumpMovement;
    //[SerializeField] private PlayerSwipeInput input;



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
}
