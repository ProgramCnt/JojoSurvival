using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackItem : Equip
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
        OnAttack();       
    }

    void OnAttack()
    {
        if (!attacking)
        {
            attacking = true;
            anim.SetTrigger("Attack");
            Invoke("OnCanAttack", data.equipmentData.attackRate);
        }
    }

    void OnCanAttack()
    {
        attacking = false;
    }

    public override void OnHitFunc(RaycastHit ray)
    {
        if (ray.collider.TryGetComponent(out IDamageable damagable))
        {
            damagable.TakePhysicalDamage(data.equipmentData.damage);
        }
    }
}
