using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IInteractable
{
    public string GetInteractPrompt();
    public void OnInteract();
}

public class Item : MonoBehaviour, IInteractable
{
    public ItemData data;

    public string GetInteractPrompt()
    {
        return $"{data.itemName}\n{data.description}";
    }

    public void OnInteract()
    {
        // 아이템 상호작용 기능
    }
}
