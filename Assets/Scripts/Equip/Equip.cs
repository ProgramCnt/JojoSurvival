using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equip : MonoBehaviour
{
    public float attackRate;
    public float attackDistance;
    public float useStamina;
    public float damage;
    public bool canCraft;
    bool attacking;

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
                Invoke("OnCanAttack", attackRate);
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

        if (Physics.Raycast(ray, out hit, attackDistance))
        {
            // ���� ����� �ڿ��̰� �ڿ��� ĳ�� �����϶�
            //if (canCraft && hit.collider.TryGetComponent())
            {

            }
            // ���� ����� NPC�ϰ��
            //else if (hit.collider.TryGetComponent())
            {
                
            }
        }
    }
}
