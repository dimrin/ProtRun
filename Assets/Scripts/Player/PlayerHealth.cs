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
            hittable.Hit();
            if(isHittable)
            {
                OnGetHit?.Invoke();
            }
        }
    }


    public void MakeUnhittable(int timerTime)
    {
        StartCoroutine(ChangeHittableState(timerTime));
    }

    private IEnumerator ChangeHittableState(int timerTime)
    {
        isHittable = false;
        Debug.Log("is not hittable");
        yield return new WaitForSeconds(timerTime);
        Debug.Log("is hittable");
        isHittable = true;
    }
}
