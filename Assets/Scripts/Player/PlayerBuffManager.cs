using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuffManager : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private PlayerLaneMovement movement;
    [SerializeField] private Transform playerTransform;

    private Dictionary<ItemType, IBuff> _buffs;



    private void Start()
    {
        _buffs = new Dictionary<ItemType, IBuff>
        {
            { ItemType.Shield, new ShieldBuff(playerHealth) },
            { ItemType.Speed, new SpeedBuff(playerHealth, movement) },
            { ItemType.Magnet, new MagnetBuff(playerTransform) }
        };
    }

    public void SetComponentsOnAwake(PlayerHealth playerHealth, PlayerLaneMovement movement, Transform playerTransform)
    {
        this.playerHealth = playerHealth;
        this.movement = movement;
        this.playerTransform = playerTransform;
    }

    public void UpdateBuffsStates()
    {
        foreach (var buff in _buffs.Values)
        {
            if (buff.IsActive)
                buff.Update();
        }
    }

    public void ApplyBuff(ItemType itemType, int value)
    {
        if (_buffs.TryGetValue(itemType, out var buff))
        {
            buff.Apply(value);
        }
        else
        {
            Debug.LogWarning($"No buff for item type: {itemType}");
        }
    }
}
