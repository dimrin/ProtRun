using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBuff : IBuff {
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
        _duration = value;
        _timer = 0f;
        IsActive = true;
        _playerHealth.ChangeHittableState(IsActive);
    }

    public void Update()
    {
        if (!IsActive) return;
        _timer += Time.deltaTime;
        if (_timer >= _duration)
        {
            IsActive = false;
            _playerHealth.ChangeHittableState(IsActive);
        }
    }
}
