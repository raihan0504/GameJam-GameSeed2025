using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    public static OrderManager instance;

    [Header("Prefab Permintaan Menu")]
    public List<GameObject> orderPrefabs; // Satu prefab per DishType

    [Header("Parent Grid")]
    public Transform orderSlotUI; // Drag ke OrderSlotUI (yang ada GridLayoutGroup-nya)

    [Header("Jumlah Permintaan")]
    public int maxOrders = 3;

    private List<GameObject> currentOrders = new();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Agar tetap ada di semua scene
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        GenerateInitialOrders();
    }

    public void GenerateInitialOrders()
    {
        for (int i = 0; i < maxOrders; i++)
        {
            SpawnRandomOrder();
        }
    }


    void SpawnRandomOrder()
    {
        if (currentOrders.Count >= maxOrders) return;

        int randomIndex = Random.Range(0, orderPrefabs.Count);
        GameObject prefab = orderPrefabs[randomIndex];

        GameObject order = Instantiate(prefab, orderSlotUI); // langsung ke OrderSlotUI (Grid)
        currentOrders.Add(order);
    }

    public bool TryServeDish(DishType servedDish)
    {
        for (int i = 0; i < currentOrders.Count; i++)
        {
            OrderUI ui = currentOrders[i].GetComponent<OrderUI>();
            if (ui != null && ui.dishType == servedDish)
            {
                Destroy(currentOrders[i]);
                currentOrders.RemoveAt(i);

                SpawnRandomOrder();
                return true;
            }
        }

        return false;
    }
}
