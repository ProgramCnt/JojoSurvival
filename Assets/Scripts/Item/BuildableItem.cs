using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.UIElements;

public class BuildableItem : Equip
{
    public override void Start()
    {
        base.Start();
        OffHitFunc();
    }

    private void Update()
    {
        OnHit();
    }

    public override void OnHitFunc(RaycastHit hit)
    {
        transform.localScale = new Vector3(1, 1, 1);
        //transform.localRotation = Quaternion.Euler(90, 0, 0);

        float offset = 0.1f;
        transform.position = hit.point + hit.normal * offset;
        transform.up = hit.normal;

        transform.rotation = Quaternion.LookRotation(transform.forward, hit.normal);
        CharacterManager.Instance.Player.controller.clickAction = OnUseItem;
    }

    public override void OffHitFunc()
    {
        transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        transform.localPosition = new Vector3(0.5f, 0.5f, 0.5f);
        CharacterManager.Instance.Player.controller.clickAction = null;
    }

    public override void OnUseItem()
    {
        transform.SetParent(null);
        GameObject houseTool = Instantiate(data.dropPrefab, transform.position, Quaternion.identity);
        CharacterManager.Instance.Player.equip.UnEquip();
    }
}
