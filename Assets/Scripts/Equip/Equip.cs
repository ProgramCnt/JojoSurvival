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
        // 테스트 코드
        if (Input.GetMouseButtonDown(0))
        {
            OnAttackInput();
        }
    }

    public void OnAttackInput()
    {
        if (!attacking)
        {
            // 플레이어 스테미너 사용 조건문
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

            // 맞은 대상이 자원이고 자원을 캐는 도구일때
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
            //// 맞은 대상이 NPC일경우
            //else if (hit.collider.TryGetComponent<IEntity>(out entity))
            //{

            //}
        }
    }
}
