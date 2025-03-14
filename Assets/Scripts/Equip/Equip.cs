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
            // 맞은 대상이 자원이고 자원을 캐는 도구일때
            //if (canCraft && hit.collider.TryGetComponent())
            {

            }
            // 맞은 대상이 NPC일경우
            //else if (hit.collider.TryGetComponent())
            {
                
            }
        }
    }
}
