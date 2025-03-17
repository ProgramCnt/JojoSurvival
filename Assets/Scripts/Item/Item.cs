using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, IInteractable
{
    public ItemData data;

    public string GetInteractPrompt()
    {
        return $"{data.itemName}\n{data.description}";
    }

    public void OnInteract()
    {
        // ������ ��ȣ�ۿ� ���
        CharacterManager.Instance.Player.itemData = data;
        CharacterManager.Instance.Player.addItem?.Invoke();
        //Destroy(gameObject);
    }
}
