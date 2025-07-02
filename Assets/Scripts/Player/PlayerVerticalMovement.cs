using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVerticalMovement : MonoBehaviour
{
    [Header("Jump Settings")]
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float gravity = -20f;
    [SerializeField] private float pushDownForce = -30f;

    private CharacterController characterController;
    private float verticalVelocity = 0f;
    private bool _isPushedDown = false;

    private bool _isGrounded = false;

    public bool IsJumping => !characterController.isGrounded;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    public void ApplyGravity()
    {
        if (characterController.isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -1f; // Keep grounded
        }
        else if (_isPushedDown)
        {
            verticalVelocity = pushDownForce;
            _isPushedDown = false;
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
        }
    }

    public void MoveUp()
    {
        Vector3 verticalMove = Vector3.up * verticalVelocity * Time.deltaTime;
        characterController.Move(verticalMove);
    }

    public void Jump(Action OnJumped)
    {
        if (!characterController.isGrounded) return;

        verticalVelocity = jumpForce;
        OnJumped?.Invoke();
    }

    public void PushDown(Action OnPushedDown)
    {
        if (characterController.isGrounded) return;
        _isPushedDown = true;
        OnPushedDown?.Invoke();
    }

}
