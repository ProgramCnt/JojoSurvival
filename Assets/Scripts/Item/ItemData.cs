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

public enum EquipType
{
    Axe,
    Pickaxe,
    Weapon,
    Buildable
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
    public float attackRate;
    public float attackDistance;
    public float useStamina;
    public int damage;
    public EquipType equipType;
    public bool canCraft;
    public LayerMask layerMask;
}

[Serializable]
public class ConsumableData
{
    public ConsumableType type;
    public float value;
}
