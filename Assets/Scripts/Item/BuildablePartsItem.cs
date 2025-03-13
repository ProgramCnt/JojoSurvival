using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class BuildablePartsItem : Item
{
    private Camera camera;
    [SerializeField] private LayerMask layerMask;
    private float buildDistance = 10f;
    private bool canInstall = false;
    public bool isInstalled = false;
    [SerializeField] Material originalMaterial;
    [SerializeField] Material changeMaterial;

    GameObject hitObject;
    Transform hitChild;
    MeshRenderer hitMesh;

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isInstalled)
        {
            OnHit();
        }
    }
    private void OnHit()
    {
        Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, buildDistance, layerMask))
        {
            hitObject = hit.collider.gameObject;
            hitChild = hitObject.transform.Find(gameObject.name);

            hitMesh = hitChild.GetComponent<MeshRenderer>();
            Material[] mats = hitMesh.materials;
            mats[0] = changeMaterial;
            hitMesh.materials = mats;

            transform.GetComponent<MeshRenderer>().enabled = false;
            CharacterManager.Instance.Player.controller.clickAction = SetParts;
        }
        else
        {
            if (hitMesh != null)
            {
                Material[] mats = hitMesh.materials;
                mats[0] = originalMaterial;
                hitMesh.materials = mats;
            }
            transform.GetComponent<MeshRenderer>().enabled = true;
            transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            transform.localPosition = new Vector3(0.8f, 1, 1);
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            CharacterManager.Instance.Player.controller.clickAction = null;
        }
    }

    private void SetParts()
    {
        Material[] mats = hitMesh.materials;
        mats[0] = changeMaterial;
        hitMesh.materials = mats;
        CharacterManager.Instance.Player.controller.clickAction = null;
        Destroy(gameObject);
    }
}
