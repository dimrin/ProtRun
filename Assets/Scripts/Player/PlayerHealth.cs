using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private bool isHittable = true;


    public void GetHit(GameObject gameObject, Action OnGetHit)
    {
        if (gameObject.TryGetComponent(out IHittable hittable))
        {
            hittable.GetHit();
            if(isHittable)
            {
                OnGetHit?.Invoke();
            }
        }
    }

    public void ChangeHittableState(bool state)
    {
        isHittable = !state;
        Debug.Log("isHittable " + isHittable);
    }
}
