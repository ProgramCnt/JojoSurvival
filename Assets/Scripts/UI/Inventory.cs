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

        // 아이템 슬롯 초기화작업
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
        // 선택된 아이템이 없을 때
        if (!selectedItem) 
        {
            if (!slots[idx].item)
                return;

            selectedIdx = idx;
            selectedItem = slots[idx].item;

            StartCoroutine(FollowItemIcon());
        }
        // 선택된 아이템이 있을 때
        else if (selectedItem) 
        {
            ItemData tempItem = selectedItem;
            int tempQuantity = slots[selectedIdx].quantity;
            bool tempEquipped = slots[selectedIdx].equipped;

            // 전에 선택된 아이템 슬롯
            slots[selectedIdx].item = slots[idx].item;
            slots[selectedIdx].quantity = slots[idx].quantity;
            slots[selectedIdx].equipped = slots[idx].equipped;

            // 이후 선택된 아이템 슬롯
            slots[idx].item = tempItem;
            slots[idx].quantity = tempQuantity;
            slots[idx].equipped = tempEquipped;

            // 아이템 장착 인덱스 변경
            curEquipIdx = slots[selectedIdx].equipped ? slots[selectedIdx].idx : slots[idx].idx;

            // UI 업데이트
            if (slots[idx].item)
                slots[idx].Set();
            else
                slots[idx].Clear();

            if (slots[selectedIdx].item)
                slots[selectedIdx].Set();
            else
                slots[selectedIdx].Clear();

            // 아이템 슬롯 교체이후 비워주기
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

            Debug.Log("버리기");
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
                    // 목마름 회복
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

    public void RemoveItem(int idx, int removeAmount)
    {
        if (slots[idx].equipped)
        {
            slots[idx].equipped = false;
            CharacterManager.Instance.Player.equip.UnEquip();
        }

        slots[idx].quantity -= removeAmount;
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

            // 지금 장착한 아이템과 장착하려는 아이템이 같으면 리턴
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

        // 중첩가능한 아이템슬롯 찾기
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

        // 빈슬롯
        slot = GetEmptySlot();
        if (slot)
        {
            slot.item = newItem;
            slot.quantity = 1;
            CharacterManager.Instance.Player.itemData = null;

            slot.Set();
            return;
        }

        // 아이템슬롯이 꽉찬 상태
        ThrowItem(newItem);
        CharacterManager.Instance.Player.itemData = null;
    }

    public void AddItem(ItemData Item)
    {
        ItemData newItem = Item;
        ItemSlot slot;

        // 중첩가능한 아이템슬롯 찾기
        if (newItem.canStack)
        {
            slot = GetStackItemSlot(newItem);
            if (slot)
            {
                slot.quantity++;
                slot.Set();
                return;
            }
        }

        // 빈슬롯
        slot = GetEmptySlot();
        if (slot)
        {
            slot.item = newItem;
            slot.quantity = 1;
            slot.Set();
            return;
        }

        // 아이템슬롯이 꽉찬 상태
        ThrowItem(newItem);
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

    public bool IsSelectedItem()
    {
        return selectedItem ? true : false;
    }

    public int GetItemQuantity(ItemData item)
    {
        int totalQuantity = 0;

        foreach (ItemSlot slot in slots)
        {
            if (slot.item == null)
                continue;

            if (slot.item == item)
            {
                totalQuantity += slot.quantity;
            }
        }

        return totalQuantity;
    }

    public int GetItemQuantity(int idx)
    {
        return slots[idx].quantity;
    }

    public int GetItemSlotIndex(ItemData item)
    {
        int SlotIdx = -1;

        foreach (ItemSlot slot in slots)
        {
            if (slot.item == null)
                continue;

            if (slot.item == item)
            {
                SlotIdx = slot.idx;
                break;
            }
        }

        return SlotIdx;
    }
}
