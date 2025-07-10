using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FollowingObjectLaneMovement))]
public class FollowingObject : MonoBehaviour, IFollowable {
    [SerializeField] private Transform target;
    [SerializeField] private FollowingObjectLaneMovement laneMovement;

    private void Awake()
    {
        if(laneMovement == null)
        {
            laneMovement = GetComponent<FollowingObjectLaneMovement>();
        }
    }

    private void Start()
    {
        laneMovement.SetTargetOnStart(target);
    }

    private void OnEnable()
    {
        PlayerSwipeInput.SwipeToLeft += laneMovement.MoveLeft;
        PlayerSwipeInput.SwipeToRight += laneMovement.MoveRight;
    }

    private void OnDisable()
    {
        PlayerSwipeInput.SwipeToLeft -= laneMovement.MoveLeft;
        PlayerSwipeInput.SwipeToRight -= laneMovement.MoveRight;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Follow();
    }

    public void Follow()
    {
        laneMovement.Follow();
    }
}
