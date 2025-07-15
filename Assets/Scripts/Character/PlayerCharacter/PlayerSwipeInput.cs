using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class PlayerSwipeInput : MonoBehaviour {
    [SerializeField] private float swipeThreshold = 50f;

    private PlayerControls inputs;
    private Vector2 startPos;
    private bool isSwiping = false;
    private bool hasSwiped = false;

    public static event Action SwipeToLeft;
    public static event Action SwipeToRight;
    public static event Action SwipeToUp;
    public static event Action SwipeToDown;

    private void Awake()
    {
        inputs = new PlayerControls();
    }

    private void OnEnable()
    {
        GameSessionManager.OnGameRun += EnableInput;
        GameSessionManager.OnGamePaused += DisableInput;
        GameSessionManager.OnGameFinished += DisableInput;
    }

    private void OnDisable()
    {
        GameSessionManager.OnGameRun -= EnableInput;
        GameSessionManager.OnGamePaused -= DisableInput;
        GameSessionManager.OnGameFinished -= DisableInput;
    }

    private void EnableInput()
    {
        inputs.Enable();
        inputs.PlayerTouchActions.TouchPress.started += _ => StartTouch();
        inputs.PlayerTouchActions.TouchPress.canceled += _ => EndTouch();
    }

    private void DisableInput()
    {
        inputs.PlayerTouchActions.TouchPress.started -= _ => StartTouch();
        inputs.PlayerTouchActions.TouchPress.canceled -= _ => EndTouch();
        inputs.Disable();
    }

    private void StartTouch()
    {
        startPos = inputs.PlayerTouchActions.TouchMovement.ReadValue<Vector2>();
        isSwiping = true;
        hasSwiped = false;
    }

    private void EndTouch()
    {
        isSwiping = false;
    }

    private void Update()
    {
        if (!isSwiping || hasSwiped) return;

        Vector2 currentPos = inputs.PlayerTouchActions.TouchMovement.ReadValue<Vector2>();
        Vector2 delta = currentPos - startPos;

        if (delta.magnitude >= swipeThreshold)
        {
            DetectSwipe(delta);
            hasSwiped = true;
        }
    }

    private void DetectSwipe(Vector2 swipe)
    {
        if (Mathf.Abs(swipe.x) > Mathf.Abs(swipe.y))
        {
            if (swipe.x > 0)
            {
                //Debug.Log("Swipe Right");
                SwipeToRight?.Invoke();
            }
            else
            {
                //Debug.Log("Swipe Left");
                SwipeToLeft?.Invoke();
            }
        }
        else
        {
            if (swipe.y > 0)
            {
                //Debug.Log("Swipe Up");
                SwipeToUp?.Invoke();
            }
            else
            {
                //Debug.Log("Swipe Down");
                SwipeToDown?.Invoke();
            }
        }
    }
    
}
