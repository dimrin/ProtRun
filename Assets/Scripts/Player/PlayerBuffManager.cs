using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuffManager : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private PlayerLaneMovement movement;
    [SerializeField] private MagnetBuff magnetBuff;

    private Dictionary<ItemType, IBuff> _buffs;

    private void Awake()
    {
        if (playerHealth == null) playerHealth = GetComponent<PlayerHealth>();
        if (movement == null) movement = GetComponent<PlayerLaneMovement>();
        if (magnetBuff == null) magnetBuff = GetComponent<MagnetBuff>();

        magnetBuff.Initialize(transform);

        _buffs = new Dictionary<ItemType, IBuff>
        {
            { ItemType.Shield, new ShieldBuff(playerHealth) },
            { ItemType.Ultimate, new UltimateBuff(playerHealth, movement) },
            { ItemType.Magnet, magnetBuff }
        };
    }

    public void ApplyBuff(ItemType itemType, int value)
    {
        if (_buffs.TryGetValue(itemType, out var buff))
        {
            buff.Apply(value);
        }
        else
        {
            Debug.LogWarning($"No buff handler for item type: {itemType}");
        }
    }
}
