using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildHouse : Item
{
    public Queue<ItemData> parts;
    // Start is called before the first frame update
    void Start()
    {
        parts = new Queue<ItemData>();
    }

    public override void OnInteract()
    {
        ReturnPart();
        base.OnInteract();
    }

    void ReturnPart()
    {
        foreach (ItemData part in parts)
        {
            Instantiate(part.dropPrefab, CharacterManager.Instance.Player.transform.position + CharacterManager.Instance.Player.controller.cameraContainer.
                transform.forward * 1.2f + CharacterManager.Instance.Player.controller.cameraContainer.transform.up * 3f, Quaternion.identity);
        }
        parts.Clear();
    }

    public void BreakParts()
    {
        foreach (ItemData part in parts)
        {
            Instantiate(part.dropPrefab, transform.position + transform.up * 1.5f, Quaternion.identity);
        }
        parts.Clear();
        Destroy(gameObject);
    }
}
