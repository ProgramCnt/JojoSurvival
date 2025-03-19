using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    public Equip curEquip;
    public Transform equipParent;

    PlayerCondition condition;
    PlayerController controller;

    private void Start()
    {
        condition = GetComponent<PlayerCondition>();
        controller = GetComponent<PlayerController>();       
    }

    public void Equip(ItemData item)
    {
        // ���� ���� �������� �ִٸ� ��������
        UnEquip();
        curEquip = Instantiate(item.equipmentData.equipPrefab, equipParent).GetComponent<Equip>();
        controller.clickAction += curEquip.OnUseItem;
    }

    public void UnEquip()
    {
        if (curEquip)
        {
            Destroy(curEquip.gameObject);
            controller.clickAction = null;
            curEquip = null;
        }
    }
}
