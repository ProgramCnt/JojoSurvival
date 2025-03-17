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

    [Header("CraftItem Info")]
    public Transform parentCraftItem;
    public GameObject craftItemPrefab;
    public Image craftItemIcon;
    public TextMeshProUGUI craftItemName;
    public TextMeshProUGUI craftItemDesc;
    public TextMeshProUGUI craftItemSpec;

    [Header("Ingredient Info")]
    public Transform parentIngredient;
    public GameObject ingredientPrefab;

    public CraftingRecipe seletedRecipe;

    private void Start()
    {
        InitCraftItemList();
    }

    private void OnDisable()
    {
        craftItemIcon.sprite = null;
        craftItemIcon.gameObject.SetActive(false);
        craftItemName.text = "";
        craftItemDesc.text = "";
        craftItemSpec.text = "";
    }

    public void InitCraftItemList()
    {
        foreach (CraftingRecipe recipe in recipes) 
        {
            CraftItem craftItem = Instantiate(craftItemPrefab, parentCraftItem).GetComponent<CraftItem>();
            craftItem.Init(recipe, this);
        }
    }

    public bool TryCraft()
    {
        if (!seletedRecipe)
            return false;

        // 레시피 재료가 있는지 인벤토리에서 검사
        foreach (Ingredient ingredient in seletedRecipe.Ingredients)
        {
            int curQuantity = inventory.GetItemQuantity(ingredient.item);

            if (curQuantity < ingredient.quantity)
                return false;
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
                    Debug.LogWarning("SlotIndex is null");
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

        return true;
    }

    public void SelectRecipe(CraftingRecipe recipe)
    {
        craftItemIcon.sprite = recipe.resultItem.icon;
        craftItemIcon.gameObject.SetActive(true);
        craftItemName.text = recipe.resultItem.itemName;
        craftItemDesc.text = recipe.resultItem.description;
        //string itemSpec;
        //switch (recipe.resultItem.type)
        //{
        //    case ItemType.Equipment:
        //        {
        //            itemSepc += 

        //            break;
        //        }
        //}
        //craftItemSpec.text = recipe.resultItem.;
    }

    void OnCraftButtonClick()
    {
        if (TryCraft())
        {
            Debug.Log("아이템 조합 성공");
        }
        else
        {
            Debug.Log("아이템 조합 실패");
        }
    }
}