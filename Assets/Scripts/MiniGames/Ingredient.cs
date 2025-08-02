//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Ingredient : MonoBehaviour
//{
//    [Header("Ingredient Type")]
//    public IngredientType ingredientType;

//    [Header("Grid Position")]
//    public int xIndex;
//    public int yIndex;

//    [Header("Match Status")]
//    public bool isMatched;

//    [Header("Movement")]
//    private Vector2 currentPos;
//    private Vector2 targetPos;
//    public bool isMoving;

//    public Ingredient(int _x, int _y)
//    {
//        xIndex = _x;
//        yIndex = _y;
//    }

//    public void SetIndicies(int _x, int _y)
//    {
//        xIndex = _x;
//        yIndex = _y;
//    }
//}

//public enum IngredientType
//{
//    egg,
//    milk,
//    onion,
//    beef,
//    pork,
//    carrot,
//    chili,
//    cheese,
//    all_spices
//}