using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBuff: IBuff
{
    private readonly PlayerHealth _playerHealth;
    private readonly PlayerLaneMovement _movement;
    private float _timer;
    private float _duration;
    public bool IsActive { get; private set; }

    public SpeedBuff(PlayerHealth playerHealth, PlayerLaneMovement movement)
    {
        _playerHealth = playerHealth;
        _movement = movement;
    }

    public void Apply(int value)
    {
        _playerHealth.MakeUnhittable(value);
        _movement.ActivateSpeedBuff(value);
        _duration = value;
        _timer = 0f;
        IsActive = true;
    }

    public void Update()
    {
        if (!IsActive) return;
        _timer += Time.deltaTime;
        if (_timer >= _duration)
        {
            IsActive = false;
        }
    }
}
