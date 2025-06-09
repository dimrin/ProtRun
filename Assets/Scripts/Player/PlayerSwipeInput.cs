using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class PlayerSwipeInput : MonoBehaviour {
    [SerializeField] private float swipeThreshold = 50f;

    private PlayerControls inputs;
    private Vector2 startPos;
    private Vector2 endPos;
    private bool isSwiping;

    public static event Action SwipeToLeft;
    public static event Action SwipeToRight;
    public static event Action SwipeToUp;
    private void Awake()
    {
        inputs = new PlayerControls();
    }

    private void OnEnable()
    {
        inputs.Enable();
        inputs.PlayerTouchActions.TouchPress.started += ctx => StartTouch();
        inputs.PlayerTouchActions.TouchPress.canceled += ctx => EndTouch();
    }

    private void OnDisable()
    {
        inputs.PlayerTouchActions.TouchPress.started -= ctx => StartTouch();
        inputs.PlayerTouchActions.TouchPress.canceled -= ctx => EndTouch();
        inputs.Disable();
    }


    private void StartTouch()
    {
        startPos = inputs.PlayerTouchActions.TouchMovement.ReadValue<Vector2>();
        isSwiping = true;
    }

    private void EndTouch()
    {
        if (!isSwiping) return;
        endPos = inputs.PlayerTouchActions.TouchMovement.ReadValue<Vector2>();
        DetectSwipe();
        isSwiping = false;
    }

    private void DetectSwipe()
    {
        Vector2 swipe = endPos - startPos;
        if (swipe.magnitude < swipeThreshold) return;

        if (Mathf.Abs(swipe.x) > Mathf.Abs(swipe.y))
        {
            if (swipe.x > 0)
            {
                Debug.Log("Swipe Right");
                Debug.Log(swipe.x);
                SwipeToRight?.Invoke();
            }
            else
            {
                Debug.Log("Swipe Left");
                Debug.Log(swipe.x);
                SwipeToLeft?.Invoke();
            }

        }
        else
        {
            if (swipe.y > 0)
            {
                Debug.Log("Swipe Up");
                SwipeToUp?.Invoke();
            }

            else
                Debug.Log("Swipe Down");
        }
    }



}
