using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCharacterMover : MonoBehaviour
{
    [SerializeField] private Transform spawnPosition;
    [SerializeField] private Transform finalPosition;

    [SerializeField] private float moveSpeed;

    private Vector3 target;

    private void Start()
    { 
        target = finalPosition.position;
    }

    public void SetFinalPosition(Transform finalPosition)
    {
        this.finalPosition = finalPosition;
    }

    public void Move()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            target,
            moveSpeed * Time.deltaTime
        );

        /*
        if (Vector3.Distance(transform.position, target) < 0.01f)
        {
            Debug.Log("Точка достигнута!");
        }
        */
    }


}
