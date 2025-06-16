using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltimateBuff: IBuff
{
    private readonly PlayerHealth _playerHealth;
    private readonly PlayerLaneMovement _movement;

    public UltimateBuff(PlayerHealth playerHealth, PlayerLaneMovement movement)
    {
        _playerHealth = playerHealth;
        _movement = movement;
    }

    public void Apply(int value)
    {
        Debug.Log("Applying Ultimate Buff");
        _playerHealth.MakeUnhittable(value);
        _movement.ActivateSpeedBuff(value);
    }
}
