using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class BuildablePartsItem : Equip
{
    [SerializeField] Material originalMaterial;
    [SerializeField] Material changeMaterial;

    GameObject hitObject;
    Transform hitChild;
    MeshRenderer hitMesh;

    string name;

    public override void Start()
    {
        base.Start();
        name = gameObject.name.Substring(6, gameObject.name.Length - 6 - 7);
    }

    private void Update()
    {
        OnHit();
    }

    public override void OnHitFunc(RaycastHit hit)
    {
        hitObject = hit.collider.gameObject;
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
        hitObject.transform.GetComponent<BuildHouse>().parts.Enqueue(data);
        CharacterManager.Instance.Player.controller.clickAction = null;
        CheckCompleteBuild();
        UIManager.Instance.Inventory.RemoveItem(UIManager.Instance.Inventory.GetItemSlotIndex(data), 1);
    }

    void CheckCompleteBuild()
    {
        foreach (Transform child in hitObject.transform)
        {
            MeshRenderer childMesh = child.transform.GetComponent<MeshRenderer>();
            Material[] childMats = childMesh.materials;
            if (childMats[0].name.Substring(0, childMats[0].name.Length - 11) == originalMaterial.name)
            {
                return;
            }
        }
        CompleteBuild();
    }

    void CompleteBuild()
    {
        foreach (Transform child in hitObject.transform)
        {
            if (child.name.Contains("Roof") || child.name.Contains("Front"))
            {
                continue;
            }
            child.GetComponent<MeshCollider>().enabled = true;
        }
        hitObject.layer = 12;
    }
}
