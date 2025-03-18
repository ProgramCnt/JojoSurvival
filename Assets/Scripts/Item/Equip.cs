using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class Equip : Item
{   
    public Camera mainCamera;
    public Action<RaycastHit> OnHitAction;
    public Action OffHitAction;

    public virtual void Start()
    {        
        mainCamera = Camera.main;
        OnHitAction += OnHitFunc;
        OffHitAction += OffHitFunc;
    }

    public virtual void OnUseItem()
    {

    }

    public virtual void OnHitFunc(RaycastHit ray)
    {

    }

    public virtual void OffHitFunc()
    {
      
    }

    public void OnHit()
    {
        Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, data.equipmentData.attackDistance, data.equipmentData.layerMask))
        {
            OnHitAction?.Invoke(hit);          
        }
        else
        {
            OffHitAction?.Invoke();
        }
        CharacterManager.Instance.Player.condition.UseStamina(data.equipmentData.useStamina);
    }
}
