using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewRecipe", menuName = "Crafting/Recipe")]
public class CraftingRecipe : ScriptableObject
{
    public List<Ingredient> Ingredients;
    public ItemData resultItem;
}

[Serializable]
public class Ingredient
{
    public ItemData item;
    public int quantity;
}