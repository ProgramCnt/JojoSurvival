using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

    List<GameObject> ingredientPool = new List<GameObject>();

    private void Start()
    {
        InitCraftItemList();
    }

    private void OnDisable()
    {
        if (craftItemIcon.sprite)
            craftItemIcon.sprite = null;
        craftItemIcon.gameObject.SetActive(false);
        craftItemName.text = "";
        craftItemDesc.text = "";
        craftItemSpec.text = "";

        foreach (GameObject Go in ingredientPool)
        {
            Go.SetActive(false);
        }
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
        // 아이템 설명
        craftItemIcon.sprite = recipe.resultItem.icon;
        craftItemIcon.gameObject.SetActive(true);
        craftItemName.text = recipe.resultItem.itemName;
        craftItemDesc.text = recipe.resultItem.description;
        craftItemSpec.text = GetItemSpec(recipe.resultItem);

        // 조합 재료 표시
        SetIngredientItem(recipe);
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

    string GetItemSpec(ItemData data)
    {
        StringBuilder sbDesc = new StringBuilder();

        switch (data.type)
        {
            case ItemType.Equipment:
                {
                    sbDesc.AppendLine($"공격력: {data.equipmentData.damage}");
                    sbDesc.AppendLine($"공격 딜레이: {data.equipmentData.attackRate}");
                    sbDesc.AppendLine($"사거리: {data.equipmentData.attackDistance}");
                    sbDesc.AppendLine($"스테미나: {data.equipmentData.useStamina}");
                    break;
                }

            case ItemType.Consumable:
                {
                    foreach (ConsumableData consumeData in data.consumableData)
                    {
                        sbDesc.AppendLine($"{consumeData.type} {consumeData.value}");
                    }
                    break;
                }
        }

        return sbDesc.ToString();
    }

    void SetIngredientItem(CraftingRecipe recipe)
    {
        int idx = 0;

        foreach (Ingredient ingredient in recipe.Ingredients)
        {
            if (ingredientPool.Count < 0)
            {
                GameObject Go = ingredientPool[idx];
                Go.GetComponent<IngredientItem>().icon.sprite = ingredient.item.icon;
                Go.GetComponent<IngredientItem>().itemName.text = ingredient.item.itemName.ToString();
                Go.GetComponent<IngredientItem>().itemCount.text = ingredient.quantity.ToString();

                Go.SetActive(true);
                idx++;
            }
            else
            {
                GameObject Go = Instantiate(ingredientPrefab, parentIngredient);
                Go.GetComponent<IngredientItem>().icon.sprite = ingredient.item.icon;
                Go.GetComponent<IngredientItem>().itemName.text = ingredient.item.itemName.ToString();
                Go.GetComponent<IngredientItem>().itemCount.text = ingredient.quantity.ToString();

                Go.SetActive(true);
                ingredientPool.Add(Go);
            }
        }
    }
}