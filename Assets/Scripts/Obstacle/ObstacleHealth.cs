using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleHealth : MonoBehaviour
{
    public void GetDamage(Action OnGetDamage)
    {
        OnGetDamage?.Invoke();
    }
}
