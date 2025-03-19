using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentObject : MonoBehaviour, IInteractable
{
    public string description;
    public ItemData lootItemData;

    public string GetInteractPrompt()
    {
        return description;
    }

    public void OnInteract()
    {
        if (!lootItemData) 
            return;

        CharacterManager.Instance.Player.itemData = lootItemData;
        CharacterManager.Instance.Player.addItem?.Invoke();
    }
}
