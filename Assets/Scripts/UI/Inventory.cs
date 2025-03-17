using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Image itemIcon;
    
    ItemSlot[] slots;
    ItemData selectedItem;
    int selectedIdx;
    int curEquipIdx;

    PlayerCondition condition;
    PlayerController controller;

    private void Start()
    {
        condition = CharacterManager.Instance.Player.condition;
        controller = CharacterManager.Instance.Player.controller;
        controller.actionInventory += ToggleInventory;
        CharacterManager.Instance.Player.addItem += AddItem;

        // ������ ���� �ʱ�ȭ�۾�
        slots = GetComponentsInChildren<ItemSlot>();
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].idx = i;
            slots[i].inventory = this;
        }

        UpdateUI();
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        StopAllCoroutines();

        selectedItem = null;
        selectedIdx = -1;
        itemIcon.gameObject.SetActive(false);
    }

    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item)
                slots[i].Set();
            else
                slots[i].Clear();
        }
    }

    public void SelectItem(int idx)
    {
        // ���õ� �������� ���� ��
        if (!selectedItem) 
        {
            if (!slots[idx].item)
                return;

            selectedIdx = idx;
            selectedItem = slots[idx].item;

            StartCoroutine(FollowItemIcon());
        }
        // ���õ� �������� ���� ��
        else if (selectedItem) 
        {
            ItemData tempItem = selectedItem;
            bool tempEquipped = slots[selectedIdx].equipped;
            int tempQuantity = slots[selectedIdx].quantity;

            // ���� ���õ� ������ ����
            slots[selectedIdx].item = slots[idx].item;
            slots[selectedIdx].quantity = slots[idx].quantity;
            slots[selectedIdx].equipped = slots[idx].equipped;

            // UI ������Ʈ
            if (slots[selectedIdx].item)
                slots[selectedIdx].Set();
            else
                slots[selectedIdx].Clear();

            // ���� ���õ� ������ ����
            slots[idx].item = tempItem;
            slots[idx].quantity = tempQuantity;
            slots[idx].equipped = tempEquipped;

            // UI ������Ʈ
            if (slots[idx].item)
                slots[idx].Set();
            else
                slots[idx].Clear();

            // ������ ���� ��ü���� ����ֱ�
            selectedItem = null;
            selectedIdx = -1;
        }
    }

    public void OnThrowButtonClick()
    {
        if (selectedItem)
        {
            ThrowItem(selectedItem);
            RemoveItem(selectedIdx);

            selectedItem = null;
            selectedIdx = -1;

            Debug.Log("������");
        }
    }

    public void OnClickUseItem(int idx)
    {
        if (!slots[idx].item)
            return;

        switch (slots[idx].item.type)
        {
            case ItemType.Consumable:
                UseConsumable(slots[idx].item);
                RemoveItem(idx);
                break;
            case ItemType.Equipment:
                EquipItem(slots[idx]);
                break;
        }
    }

    void UseConsumable(ItemData item)
    {
        foreach (ConsumableData data in item.consumableData)
        {
            switch (data.type)
            {
                case ConsumableType.Health:
                    condition.Heal(data.value);
                    break;
                case ConsumableType.Hunger:
                    condition.Eat(data.value);
                    break;
                case ConsumableType.Thirst:
                    // �񸶸� ȸ��
                    break;
            }
        }
    }

    void RemoveItem(int idx)
    {
        if (slots[idx].equipped)
        {
            slots[idx].equipped = false;
            CharacterManager.Instance.Player.equip.UnEquip();
        }

        slots[idx].quantity--;
        if (slots[idx].quantity <= 0)
        {
            slots[idx].Clear();
        }
        else
        {
            slots[idx].Set();
        }
    }

    void EquipItem(ItemSlot slot)
    {
        if (slots[curEquipIdx].equipped)
        {
            slots[curEquipIdx].equipped = false;
            CharacterManager.Instance.Player.equip.UnEquip();
            slots[curEquipIdx].Set();

            // ���� ������ �����۰� �����Ϸ��� �������� ������ ����
            if (slots[curEquipIdx].item == slot.item)
                return;
        }

        curEquipIdx = slot.idx;
        slot.equipped = true;
        CharacterManager.Instance.Player.equip.Equip(slot.item);
        slot.Set();
    }

    IEnumerator FollowItemIcon()
    {
        itemIcon.gameObject.SetActive(true);
        itemIcon.sprite = selectedItem.icon;

        while(selectedItem)
        {
            Vector2 mousePos = Input.mousePosition;
            itemIcon.rectTransform.position = mousePos;

            yield return null;
        }

        itemIcon.gameObject.SetActive(false);
    }

    void ToggleInventory()
    {
        if (gameObject.activeInHierarchy)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }

    void AddItem()
    {
        ItemData newItem = CharacterManager.Instance.Player.itemData;
        ItemSlot slot;

        // ��ø������ �����۽��� ã��
        if (newItem.canStack)
        {
            slot = GetStackItemSlot(newItem);
            if (slot)
            {
                slot.quantity++;
                CharacterManager.Instance.Player.itemData = null;

                slot.Set();
                return;
            }
        }

        // �󽽷�
        slot = GetEmptySlot();
        if (slot)
        {
            slot.item = newItem;
            slot.quantity = 1;
            CharacterManager.Instance.Player.itemData = null;

            slot.Set();
            return;
        }

        // �����۽����� ���� ����
        ThrowItem(newItem);
        CharacterManager.Instance.Player.itemData = null;
    }

    ItemSlot GetStackItemSlot(ItemData item)
    {
        foreach (ItemSlot slot in slots)
        {
            if (slot.item == item && slot.quantity < slot.item.maxStackAmount)
            {
                return slot;
            }
        }

        return null;
    }

    ItemSlot GetEmptySlot()
    {
        foreach(ItemSlot slot in slots)
        {
            if (!slot.item)
            {
                return slot;
            }
        }

        return null;
    }

    void ThrowItem(ItemData item)
    {
        Transform playerPos = CharacterManager.Instance.Player.transform;
        Vector3 dropPos = playerPos.position + playerPos.forward + playerPos.up;

        Instantiate(item.dropPrefab, dropPos, Quaternion.Euler(Vector3.one * Random.value * 360));
    }
}
