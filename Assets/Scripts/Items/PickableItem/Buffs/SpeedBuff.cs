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
        _duration = value;
        _timer = 0f;
        IsActive = true;
        _playerHealth.ChangeHittableState(IsActive);
        _movement.ChangeBuffActivationState(IsActive);
    }

    public void Update()
    {
        if (!IsActive) return;
        _timer += Time.deltaTime;
        if (_timer >= _duration)
        {
            IsActive = false;
            _playerHealth.ChangeHittableState(IsActive);
            _movement.ChangeBuffActivationState(IsActive);
        }
    }
}
