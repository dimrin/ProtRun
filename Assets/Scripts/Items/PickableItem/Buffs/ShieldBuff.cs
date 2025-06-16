using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBuff: IBuff
{
    private readonly PlayerHealth _playerHealth;

    public ShieldBuff(PlayerHealth playerHealth)
    {
        _playerHealth = playerHealth;
    }

    public void Apply(int value)
    {
        Debug.Log("Applying Shield Buff");
        _playerHealth.MakeUnhittable(value);
    }
}
