using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BuildableItem : Item
{
    private Camera camera;
    [SerializeField] private LayerMask layerMask;
    private float buildDistance = 10f;

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        OnHit();
    }

    public void OnHit()
    {
        Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, buildDistance, layerMask))
        {
            transform.localScale = new Vector3(1, 1, 1);
            //transform.localRotation = Quaternion.Euler(90, 0, 0);

            float offset = 0.1f;
            transform.position = hit.point + hit.normal * offset;
            transform.up = hit.normal;

            transform.rotation = Quaternion.LookRotation(transform.forward, hit.normal);
        }
        else
        {
            transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            transform.localPosition = new Vector3(0.8f, 1, 1);
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
