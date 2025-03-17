using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftItem : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI itemName;

    public CraftingRecipe recipe;
    public CraftingManager craftingManager;

    public void Init(CraftingRecipe recipe, CraftingManager manager)
    {
        this.recipe = recipe;
        craftingManager = manager;

        icon.sprite = recipe.resultItem.icon;
        itemName.text = recipe.resultItem.itemName;
    }

    public void OnButtonClick()
    {
        if (!craftingManager)
        {
            Debug.Log("CraftingManager is Null");
            return;
        }

        craftingManager.SelectRecipe(recipe);
    }
}
