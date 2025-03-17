using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class CraftItem : Equip
{
    bool attacking;

    Animator anim;

    public override void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
    }

    public override void OnUseItem()
    {
        base.OnUseItem();
        OnCraft();
    }

    void OnCraft()
    {
        if (!attacking)
        {
            attacking = true;
            anim.SetTrigger("Attack");
            Invoke("OnCanCraft", data.equipmentData.attackRate);           
        }
    }

    void OnCanCraft()
    {               
        attacking = false;
    }

    public override void OnHitFunc(RaycastHit hit)
    {
        // 맞은 대상이 자원이고 자원을 캐는 도구일때
        if (hit.collider.TryGetComponent<Entity>(out Entity entity))
        {
            if (data.equipmentData != null)
            {
                if (data.equipmentData.canCraft && data.equipmentData.equipType == EquipType.Axe)
                {
                    if (entity.entityType == EntityType.Tree)
                    {
                        entity.OnTakeDamage(hit);
                    }
                }
                else if (data.equipmentData.canCraft && data.equipmentData.equipType == EquipType.Pickaxe)
                {
                    if (entity.entityType == EntityType.Rock)
                    {
                        entity.OnTakeDamage(hit);
                    }
                }
            }
        }
    }
}
