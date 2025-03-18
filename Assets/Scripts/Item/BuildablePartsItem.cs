using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class BuildablePartsItem : Equip
{
    [SerializeField] Material originalMaterial;
    [SerializeField] Material changeMaterial;

    GameObject hitObject;
    Transform hitChild;
    MeshRenderer hitMesh;

    public override void Start()
    {
        base.Start();
    }

    private void Update()
    {
        OnHit();
    }

    public override void OnHitFunc(RaycastHit hit)
    {
        hitObject = hit.collider.gameObject;
        string name = gameObject.name.Substring(6, gameObject.name.Length - 6 - 7);
        Debug.Log(name);
        Debug.Log(gameObject.name);
        hitChild = hitObject.transform.Find(name);

        hitMesh = hitChild.GetComponent<MeshRenderer>();
        Material[] mats = hitMesh.materials;
        mats[0] = changeMaterial;
        hitMesh.materials = mats;

        transform.GetComponent<MeshRenderer>().enabled = false;
        CharacterManager.Instance.Player.controller.clickAction = OnUseItem;
    }


    public override void OffHitFunc()
    {
        if (hitMesh != null)
        {
            Material[] mats = hitMesh.materials;
            mats[0] = originalMaterial;
            hitMesh.materials = mats;
        }
        transform.GetComponent<MeshRenderer>().enabled = true;
        transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        transform.localPosition = new Vector3(0.5f, 0.5f, 0.5f);
        transform.localRotation = Quaternion.Euler(0, 0, 0);
        CharacterManager.Instance.Player.controller.clickAction = null;
    }

    public override void OnUseItem()
    {
        Material[] mats = hitMesh.materials;
        mats[0] = changeMaterial;
        hitMesh.materials = mats;
        CharacterManager.Instance.Player.controller.clickAction = null;
        Debug.Log(UIManager.Instance.Inventory.GetItemSlotIndex(data));
        UIManager.Instance.Inventory.RemoveItem(UIManager.Instance.Inventory.GetItemSlotIndex(data),1);
        //CharacterManager.Instance.Player.equip.UnEquip();
    }
}
