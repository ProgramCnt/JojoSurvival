using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equip : MonoBehaviour
{
    bool attacking;
    public ItemData itemData;

    Animator anim;
    Camera mainCamera;

    void Start()
    {
        anim = GetComponent<Animator>();
        mainCamera = Camera.main;
    }

    void Update()
    {
        // �׽�Ʈ �ڵ�
        if (Input.GetMouseButtonDown(0))
        {
            OnAttackInput();
        }
    }

    public void OnAttackInput()
    {
        if (!attacking)
        {
            // �÷��̾� ���׹̳� ��� ���ǹ�
            //if ()
            {
                attacking = true;
                anim.SetTrigger("Attack");
                Invoke("OnCanAttack", itemData.equipmentData.attackRate);
            }
        }
    }

    void OnCanAttack()
    {
        attacking = false;
    }

    public void OnHit()
    {
        Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, itemData.equipmentData.attackDistance))
        {

            // ���� ����� �ڿ��̰� �ڿ��� ĳ�� �����϶�
            if (hit.collider.TryGetComponent<Entity>(out Entity entity))
            {
                if (itemData.equipmentData != null)
                {
                    if (itemData.equipmentData.canCraft && itemData.equipmentData.equipType == EquipType.Axe)
                    {
                        if (entity.entityType == EntityType.Tree)
                        {
                            entity.OnTakeDamage(itemData.equipmentData.damage);
                        }
                        else
                        {
                            entity.OnTakeDamage(0);
                        }
                    }
                    else if (itemData.equipmentData.canCraft && itemData.equipmentData.equipType == EquipType.Pickaxe)
                    {
                        if (entity.entityType == EntityType.Rock)
                        {
                            entity.OnTakeDamage(itemData.equipmentData.damage);
                        }
                        else
                        {
                            entity.OnTakeDamage(0);
                        }
                    }
                    else
                    {
                        entity.OnTakeDamage(0);
                    }
                }
            }
            //if (itemData.equipmentData.canCraft && hit.collider.TryGetComponent<IEntity>(out entity))
            //{
            //    entity.OnTakeDamage(itemData.equipmentData.damage);
            //}
            //// ���� ����� NPC�ϰ��
            //else if (hit.collider.TryGetComponent<IEntity>(out entity))
            //{

            //}
        }
    }
}
