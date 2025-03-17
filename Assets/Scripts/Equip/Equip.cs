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
            attacking = true;
            Invoke("OnCanAttack", itemData.equipmentData.attackRate);
        }
    }

    void OnCanAttack()
    {
        anim.SetTrigger("Attack");
        CharacterManager.Instance.Player.controller.clickAction?.Invoke();
        attacking = false;
    }

    public void OnHit()
    {
        Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, itemData.equipmentData.attackDistance, itemData.equipmentData.layerMask))
        {

            // 맞은 대상이 자원이고 자원을 캐는 도구일때
            IEntity entity;
            if (hit.collider.TryGetComponent<IEntity>(out entity))
            {
                entity.OnTakeDamage(itemData.equipmentData.damage);
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
