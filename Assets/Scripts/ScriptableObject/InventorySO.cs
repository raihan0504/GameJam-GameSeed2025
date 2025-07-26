using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InventoryData", menuName = "SO/Game/Inventory")]
public class InventorySO : ScriptableObject
{
    public List<string> collectedIngredients = new List<string>();

    public void AddIngredient(string ingredient)
    {
        collectedIngredients.Add(ingredient);
    }
}
