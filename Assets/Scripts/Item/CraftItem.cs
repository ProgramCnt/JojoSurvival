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
        if (hit.collider.TryGetComponent<Entity>(out Entity entity))
        {
            entity.OnTakeDamage(hit);
        }
        else if (hit.collider.TryGetComponent<BuildHouse>(out BuildHouse buildHouse))
        {
            buildHouse.BreakParts();
        }
    }
}
