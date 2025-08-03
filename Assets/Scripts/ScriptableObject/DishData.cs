using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Cooking/Dish Recipe")]
public class DishData : ScriptableObject
{
    public DishType dishType;
    public GameObject dishPrefab;
    public List<PotionType> requiredIngredients;
}
