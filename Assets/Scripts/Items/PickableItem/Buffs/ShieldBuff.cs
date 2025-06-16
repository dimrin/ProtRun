using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBuff: IBuff
{
    private readonly PlayerHealth _playerHealth;
    private float _timer;
    private float _duration;
    public bool IsActive { get; private set; }

    public ShieldBuff(PlayerHealth playerHealth)
    {
        _playerHealth = playerHealth;
    }

    public void Apply(int value)
    {
        _playerHealth.MakeUnhittable(value);
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
