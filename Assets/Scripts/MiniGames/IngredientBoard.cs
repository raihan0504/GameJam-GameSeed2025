using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class IngredientBoard : MonoBehaviour
{
    [Header("Size of the Board")]
    public int width = 6;
    public int height = 8;

    [Header("Spacing of the Board")]
    public float spacingX;
    public float spacingY;

    [Header("Reference Prefab")]
    public GameObject[] ingredientPrefab;

    [Header("Reference the collection board")]
    public GameObject ingredientBoardGo;
    private Node[,] ingredientBoard;

    // [Header("Layout Array")]
    // public ArrayLayout arrayLayout;

    [Header("Static of ingredientBoard")]
    public static IngredientBoard Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        InitializeBoard();
    }

    void InitializeBoard()
    {
        ingredientBoard = new Node[width, height];

        spacingX = (float)(width - 1) / 2;
        spacingY = (float)(height - 1) / 2;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Vector2 position = new Vector2(x - spacingX, y - spacingY);

                int randomIndex = Random.Range(0, ingredientPrefab.Length);

                GameObject ingredient = Instantiate(ingredientPrefab[randomIndex], position, Quaternion.identity);
                ingredient.GetComponent<Ingredient>().SetIndicies(x, y);
                ingredientBoard[x, y] = new Node(true, ingredient);
            }
        }
        if (CheckBoard())
        {
            Debug.Log("ada yang sama");
            InitializeBoard();
        }
        else
        {
            Debug.Log("gak ada yang mirip game dimulai");
        }
    }

    public bool CheckBoard()
    {
        Debug.Log("Checking board"); 
        bool hasMatched = false;

        List<Ingredient> ingredientToRemove = new();

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (ingredientBoard[x, y].isUsable)
                {
                    Ingredient ingredient = ingredientBoard[x, y].ingredient.GetComponent<Ingredient>();

                    if (!ingredient.isMatched)
                    {
                        MatchResult matchedIngredients = isConnected(ingredient);
                        if (matchedIngredients.connectedIngredients.Count >= 3)
                        {
                            // Complex matching
                            ingredientToRemove.AddRange(matchedIngredients.connectedIngredients);

                            foreach (Ingredient pot in matchedIngredients.connectedIngredients)
                                pot.isMatched = true;

                            hasMatched = true;
                            
                        }
                    }
                }
            }
        }
        return hasMatched;
    }

    MatchResult isConnected(Ingredient ingredient)
    {
        List<Ingredient> connectedIngredients = new();
        IngredientType ingredientType = ingredient.ingredientType;

        connectedIngredients.Add(ingredient);
        // Check right
        CheckDirection(ingredient, new Vector2Int(1, 0), connectedIngredients);
        // Check left
        CheckDirection(ingredient, new Vector2Int(-1, 0), connectedIngredients);
        // checking 3 match (horzontal)
        if (connectedIngredients.Count == 3)
        {
            Debug.Log("normal horizontal" + connectedIngredients[0].ingredientType);

            return new MatchResult
            {
                connectedIngredients = connectedIngredients,
                direction = MatchDirection.horizontal
            };
        }
        // checking > 3 (long horizontal)
        else if(connectedIngredients.Count > 3)
        {
            Debug.Log("long horizontal match" + connectedIngredients[0].ingredientType);

            return new MatchResult
            {
                connectedIngredients = connectedIngredients,
                direction = MatchDirection.longHorizontal
            };
        }
        connectedIngredients.Clear();
        connectedIngredients.Add(ingredient);



        // Check Up
        CheckDirection(ingredient, new Vector2Int(0, 1), connectedIngredients);
        // Check Down
        CheckDirection(ingredient, new Vector2Int(0, -1), connectedIngredients);

        // Checking vertial 3 match
        if (connectedIngredients.Count == 3)
        {
            Debug.Log("normal vertical" + connectedIngredients[0].ingredientType);

            return new MatchResult
            {
                connectedIngredients = connectedIngredients,
                direction = MatchDirection.vertical
            };
        }
        // Checking long vertival
        else if (connectedIngredients.Count > 3)
        {
            Debug.Log("long vertical match" + connectedIngredients[0].ingredientType);

            return new MatchResult
            {
                connectedIngredients = connectedIngredients,
                direction = MatchDirection.longVertical
            };
        }
        else
        {
            return new MatchResult
            {
                connectedIngredients = connectedIngredients,
                direction = MatchDirection.none
            };
        }
    }


    void CheckDirection(Ingredient ingre, Vector2Int direction, List<Ingredient> connectedIngredients)
    {
        IngredientType ingredientType = ingre.ingredientType;
        int x = ingre.xIndex + direction.x;
        int y = ingre.yIndex + direction.y;

        while (x >= 0 && x < width && y >= 0 && y < height)
        {
            if (ingredientBoard[x,y].isUsable)
            {
                Ingredient neighbourIngredient = ingredientBoard[x, y].ingredient.GetComponent<Ingredient>();
                if (!neighbourIngredient.isMatched && neighbourIngredient.ingredientType == ingredientType)
                {
                    connectedIngredients.Add(neighbourIngredient);

                    x += direction.x;
                    y += direction.y;
                } else
                {
                    break;
                }
            } else
            {
                break;
            }
        }
    }
}

public class MatchResult
{
    public List<Ingredient> connectedIngredients;
    public MatchDirection direction;
}

public enum MatchDirection
{
    vertical,
    horizontal,
    longVertical,
    longHorizontal,
    super,
    none
}
