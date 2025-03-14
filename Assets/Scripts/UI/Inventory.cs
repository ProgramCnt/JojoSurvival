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

    private void Start()
    {
        slots = GetComponentsInChildren<ItemSlot>();
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].idx = i;
            slots[i].inventory = this;
        }

        UpdateUI();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleInventory();
        }
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
            bool tempEquipped = slots[selectedIdx].equipped;
            int tempQuantity = slots[selectedIdx].quantity;

            // 전에 선택된 아이템 슬롯
            slots[selectedIdx].item = slots[idx].item;
            slots[selectedIdx].quantity = slots[idx].quantity;
            slots[selectedIdx].equipped = slots[idx].equipped;

            // UI 업데이트
            if (slots[selectedIdx].item)
                slots[selectedIdx].Set();
            else
                slots[selectedIdx].Clear();

            // 이후 선택된 아이템 슬롯
            slots[idx].item = tempItem;
            slots[idx].quantity = tempQuantity;
            slots[idx].equipped = tempEquipped;

            // UI 업데이트
            if (slots[idx].item)
                slots[idx].Set();
            else
                slots[idx].Clear();

            // 아이템 슬롯 교체이후 비워주기
            selectedItem = null;
            selectedIdx = -1;
        }
    }

    public void OnThrowButtonClick()
    {
        Debug.Log("버리기");
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
                Equipment(slots[idx]);
                break;
        }
    }

    void UseConsumable(ItemData item)
    {
        for (int i = 0; i < item.consumableData.Length; i++)
        {
            switch (item.consumableData[i].type)
            {
                case ConsumableType.Health:
                    
                    break;
                case ConsumableType.Hunger:
                    
                    break;
                case ConsumableType.Thirst:

                    break;
            }
        }
    }

    void RemoveItem(int idx)
    {
        if (slots[idx].equipped)
        {
            slots[idx].equipped = false;
            //장착 해제
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

    void Equipment(ItemSlot slot)
    {
        if (slots[curEquipIdx].equipped)
        {
            slots[curEquipIdx].equipped = false;
            // 현재 장착중인 장비 해제
        }

        curEquipIdx = slot.idx;
        slot.equipped = true;
        //장비 장착
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
}
