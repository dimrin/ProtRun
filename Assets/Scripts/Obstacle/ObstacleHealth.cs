using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclePooler : MonoBehaviour
{
    public void GetDamage(Action OnGetDamage)
    {
        OnGetDamage?.Invoke();
    }

    public void DeactivateOnDamage()
    {
        gameObject.SetActive(false);
    }
}
