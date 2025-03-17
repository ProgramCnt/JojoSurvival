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

            // ���� ����� �ڿ��̰� �ڿ��� ĳ�� �����϶�
            IEntity entity;
            if (hit.collider.TryGetComponent<IEntity>(out entity))
            {
                entity.OnTakeDamage(itemData.equipmentData.damage);
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
