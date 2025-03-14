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
        controller.clickAction += OnAttackInput;
    }

    public void Equip(ItemData item)
    {
        // 장착 중인 아이템이 있다면 장착해제
        UnEquip();
        curEquip = Instantiate(item.equipmentData.equipPrefab, equipParent).GetComponent<Equip>();
    }

    public void UnEquip()
    {
        if (curEquip)
        {
            Destroy(curEquip.gameObject);
            curEquip = null;
        }
    }

    void OnAttackInput()
    {
        if (curEquip)
            curEquip.OnAttackInput();
    }
}
