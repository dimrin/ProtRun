using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpMovement : MonoBehaviour
{
    [Header("Jump Settings")]
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float gravity = -20f;

    private CharacterController characterController;
    private float verticalVelocity = 0f;

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
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
        }
    }

    public void MoveVertical()
    {
        Vector3 verticalMove = Vector3.up * verticalVelocity * Time.deltaTime;
        characterController.Move(verticalMove);
    }

    public void Jump()
    {
        if (!characterController.isGrounded) return;

        verticalVelocity = jumpForce;
    }
}
