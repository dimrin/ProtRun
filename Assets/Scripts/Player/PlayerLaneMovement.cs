using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class PlayerLaneMovement : MonoBehaviour {

    [Header("Components")]
    [SerializeField] private CharacterController characterController;

    [Header("Speed Settings")]
    [SerializeField] private float forwardSpeed = 10f;
    [SerializeField] private float maxSpeed = 20f;
    [SerializeField] private float speedModifier = 0.5f;
    [SerializeField] private float buffSpeed = 30f;

    [Header("Lane Settings")]
    [SerializeField] private float laneDistance = 2.5f; // Distance between lanes
    private int desiredLane = 0; // -1 (left), 0 (center), 1 (right)

    private Vector3 targetPosition;
    private float currentSpeed;

    private bool isBuffActivated = false;

    private void Awake()
    {
        if (!characterController)
            characterController = GetComponent<CharacterController>();

        UpdateTargetPosition();
    }


    public void UpdateTargetPosition()
    {
        targetPosition = transform.position.z * Vector3.forward;
        targetPosition += Vector3.right * desiredLane * laneDistance;
        targetPosition.y = transform.position.y;
    }

    public void Move()
    {
        Vector3 lateralDiff = targetPosition - transform.position;
        lateralDiff.z = 0; // don't let it affect forward movement

        Vector3 lateralMove = Vector3.zero;

        if (lateralDiff.sqrMagnitude > 0.01f)
        {
            lateralMove = lateralDiff.normalized * 25f * Time.deltaTime;

            if (lateralMove.sqrMagnitude > lateralDiff.sqrMagnitude)
                lateralMove = lateralDiff;
        }

        Vector3 forwardMove = Vector3.forward * forwardSpeed * Time.deltaTime;
        Vector3 finalMove = lateralMove + forwardMove;

        characterController.Move(finalMove);
    }

    public void IncreaseSpeed()
    {
        if (forwardSpeed < maxSpeed && !isBuffActivated)
        {
            forwardSpeed += speedModifier * Time.deltaTime;
            currentSpeed = forwardSpeed;
        }

    }

    public void ActivateSpeedBuff(int timerTime)
    {
        StartCoroutine(StartBuffDuration(timerTime));
    }

    private IEnumerator StartBuffDuration(int timerTime)
    {
        isBuffActivated = true;
        forwardSpeed = buffSpeed;
        yield return new WaitForSeconds(timerTime);
        isBuffActivated = false;
        forwardSpeed = currentSpeed;
    }

    public void MoveLeft()
    {
        desiredLane = Mathf.Max(-1, desiredLane - 1);
    }

    public void MoveRight()
    {
        desiredLane = Mathf.Min(1, desiredLane + 1);
    }

}
