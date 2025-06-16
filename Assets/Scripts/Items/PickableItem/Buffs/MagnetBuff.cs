using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetBuff : MonoBehaviour, IBuff {
    [SerializeField] private float attractionRadius = 5f;
    [SerializeField] private float attractionSpeed = 10f;

    private Transform _playerTransform;
    private bool _isActive;
    private float _duration;
    private float _timer;

    public void Initialize(Transform playerTransform)
    {
        _playerTransform = playerTransform;
    }

    public void Apply(int duration)
    {
        Debug.Log("Activating Magnet Buff");
        _isActive = true;
        _duration = duration;
        _timer = 0f;
    }

    private void Update()
    {
        if (!_isActive) return;

        _timer += Time.deltaTime;
        if (_timer > _duration)
        {
            _isActive = false;
            return;
        }

        AttractNearbyItems();
    }

    private void AttractNearbyItems()
    {
        Collider[] colliders = Physics.OverlapSphere(_playerTransform.position, attractionRadius);
        foreach (var col in colliders)
        {
            if (col.TryGetComponent<IPickable>(out var pickable))
            {
                if (pickable is Item item)
                {
                    if (item.GetItemType() == ItemType.Value)
                    {
                        Vector3 dir = (_playerTransform.position - col.transform.position).normalized;
                        col.transform.position += dir * attractionSpeed * Time.deltaTime;
                    }
                }

            }
        }
    }
}
