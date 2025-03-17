using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class CraftingManager : MonoBehaviour
{
    public List<CraftingRecipe> recipes;
    public Inventory inventory;
    
    public CraftingRecipe seletedRecipe;

    private void Start()
    {
        
    }

    public void ShowCraftList()
    {
        
    }

    public void TryCraft()
    {
        if (!seletedRecipe)
            return;

        // ������ ��ᰡ �ִ��� �κ��丮���� �˻�
        foreach (Ingredient ingredient in seletedRecipe.Ingredients)
        {
            int curQuantity = inventory.GetItemQuantity(ingredient.item);

            if (curQuantity < ingredient.quantity)
                return;
        }

        // ������ ��ᰡ �ִٸ�
        foreach (Ingredient ingredient in seletedRecipe.Ingredients)
        {
            // �κ��丮���� ��� �� ��ŭ ������ ����
            int removeQuantity = ingredient.quantity;

            while (removeQuantity > 0)
            {
                int slotIdx = inventory.GetItemSlotIndex(ingredient.item);
                if (slotIdx == -1)
                {
                    Debug.Log("SlotIndex is null");
                    break;
                }

                int slotItemQuantity = inventory.GetItemQuantity(slotIdx);
                if (slotItemQuantity < removeQuantity)
                {
                    inventory.RemoveItem(slotIdx, slotItemQuantity);
                    removeQuantity -= slotItemQuantity;
                }
                else
                {
                    inventory.RemoveItem(slotIdx, removeQuantity);
                    removeQuantity = 0;
                }
            }
        }

        // ������ ��������� �κ��丮�� �߰�
        inventory.AddItem(seletedRecipe.resultItem);

        return;
    }
}