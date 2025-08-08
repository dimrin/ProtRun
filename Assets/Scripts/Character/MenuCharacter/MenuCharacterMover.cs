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
        //transform.position = spawnPosition; // Ставим в начало
        target = finalPosition.position; // Ставим цель
    }

    public void SetFinalPosition(Transform finalPosition)
    {
        this.finalPosition = finalPosition;
    }

    public void Move()
    {
        // Двигаемся к цели
        transform.position = Vector3.MoveTowards(
            transform.position,
            target,
            moveSpeed * Time.deltaTime
        );

        // Проверка на достижение точки
        if (Vector3.Distance(transform.position, target) < 0.01f)
        {
            Debug.Log("Точка достигнута!");
            // Здесь можно запустить что-то ещё, например:
            // target = pointA.position; // чтобы двигаться обратно
        }
    }


}
