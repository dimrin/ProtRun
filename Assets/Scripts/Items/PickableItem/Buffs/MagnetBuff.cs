using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetBuff : IBuff {
    private readonly Transform _playerTransform;
    private readonly float _radius;
    private readonly float _speed;
    private float _timer;
    private float _duration;
    public bool IsActive { get; private set; }

    public MagnetBuff(Transform playerTransform, float radius = 5f, float speed = 15f)
    {
        _playerTransform = playerTransform;
        _radius = radius;
        _speed = speed;
    }

    public void Apply(int value)
    {
        _duration = value;
        _timer = 0f;
        IsActive = true;
    }

    public void Update(Action OnBuffEnded)
    {
        if (!IsActive) return;

        _timer += Time.deltaTime;
        if (_timer >= _duration)
        {
            IsActive = false;
            OnBuffEnded?.Invoke();
            return;
        }

        AttractNearbyItems();
    }

    private void AttractNearbyItems()
    {
        Collider[] colliders = Physics.OverlapSphere(_playerTransform.position, _radius);
        foreach (var col in colliders)
        {
            if (col.TryGetComponent(out IPickable pickable))
            {
                if (pickable is Item item)
                {
                    if (item.GetItemType() == ItemType.Value)
                    {
                        Vector3 direction = (_playerTransform.position - col.transform.position).normalized;
                        col.transform.position += direction * _speed * Time.deltaTime;
                    }
                }

            }
        }
    }
}
