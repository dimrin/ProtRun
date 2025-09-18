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

    void LateUpdate()
    {
        Follow();
    }

    public void Follow()
    {   float distace = transform.position.z - target.position.z;

        //Debug.Log("Distance: " + distace);
        if (distace < 2f)
        {
            Debug.Log("To Faar");
            laneMovement.SpeedUp();
        }
        else if (distace > 2f)
        {
            laneMovement.SetBaseSpeedModifier();
        }

        laneMovement.Follow();
        
       
    }

    public void SetTargetToFollow(GameObject targetToFollow)
    {
        target = targetToFollow.transform;
        laneMovement.SetTargetOnStart(target);
    }
}
