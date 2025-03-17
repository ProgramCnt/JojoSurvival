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

        // 레시피 재료가 있는지 인벤토리에서 검사
        foreach (Ingredient ingredient in seletedRecipe.Ingredients)
        {
            int curQuantity = inventory.GetItemQuantity(ingredient.item);

            if (curQuantity < ingredient.quantity)
                return;
        }

        // 레시피 재료가 있다면
        foreach (Ingredient ingredient in seletedRecipe.Ingredients)
        {
            // 인벤토리에서 재료 수 만큼 아이템 삭제
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

        // 레시피 결과아이템 인벤토리에 추가
        inventory.AddItem(seletedRecipe.resultItem);

        return;
    }
}