using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    ItemSlot[] slots;

    ItemData selectedItem;
    int selectedIdx;

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
        if (!selectedItem)
        {
            if (!slots[idx].item)
                return;

            selectedIdx = idx;
            selectedItem = slots[idx].item;
        }
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
}
