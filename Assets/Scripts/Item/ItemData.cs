using System;
using UnityEngine;

public enum ItemType
{
    Equipment,
    Consumable,
    Building,
    Resource
}

public enum ConsumableType
{
    Health,
    Hunger,
    Thirst
}

[CreateAssetMenu(fileName = "Item", menuName = "Item/New Item")]
public class ItemData : ScriptableObject
{
    [Header("Info")]
    public string itemName;
    public string description;
    public Sprite icon;
    public GameObject dropPrefab;
    public ItemType type;

    [Header("Stacking")]
    public bool canStack;
    public int maxStackAmount;

    public EquipmentData equipmentData;
    public ConsumableData[] consumableData;
}

[Serializable]
public class EquipmentData
{
    public GameObject equipPrefab;
}

[Serializable]
public class ConsumableData
{
    public ConsumableType type;
    public float value;
}
