using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {
    [SerializeField] private bool isHittable = true;

    private int currentNumberOfUnhittableStates = 0;

    public void GetHit(GameObject gameObject, Action OnCrushObstacle, Action OnGetHit)
    {
        if (gameObject.TryGetComponent(out IHittable hittable))
        {
            hittable.GetHit();
            OnCrushObstacle?.Invoke();
            if (isHittable)
            {
                OnGetHit?.Invoke();
            }
        }
    }
    /*
    public void ChangeHittableState(bool state)
    {
        isHittable = !state;

        Debug.Log("isHittable " + isHittable);
    }
    */
    public void MakeUnhittable()
    {
        isHittable = false;
        currentNumberOfUnhittableStates++;
    }

    public void MakeHittable()
    {
        currentNumberOfUnhittableStates--;
        if(currentNumberOfUnhittableStates == 0)
        {
            isHittable = true;
        }
    }
}
