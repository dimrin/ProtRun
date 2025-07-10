using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingObjectLaneMovement : MonoBehaviour
{
    [SerializeField] private float laneOffset = 2f; 
    [SerializeField] private float laneChangeSpeed = 10f; 

    private Vector3 offset;
    private int currentLane = 0; 
    private float targetX; 

    private Transform target;

    public void SetTargetOnStart(Transform target)
    {
        this.target = target;
        SetOffset();
    }

    private void SetOffset()
    {
        offset = transform.position - target.position;
        targetX = transform.position.x;
    }

    public void Follow()
    {
        float newZ = target.position.z + offset.z;
        Vector3 desiredPosition = new Vector3(targetX, transform.position.y, newZ);

        transform.position = Vector3.Lerp(transform.position, desiredPosition, laneChangeSpeed * Time.deltaTime);
    }

    
    public void MoveLeft()
    {
        if (currentLane > -1)
        {
            currentLane--;
            UpdateLanePosition();
        }
    }

    public void MoveRight()
    {
        if (currentLane < 1)
        {
            currentLane++;
            UpdateLanePosition();
        }
    }

    private void UpdateLanePosition()
    {
        targetX = currentLane * laneOffset;
    }
}

