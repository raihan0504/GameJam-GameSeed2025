using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    // Menyimpan jumlah item berdasarkan tipe PotionType
    private Dictionary<PotionType, int> inventory = new();
   
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Agar tetap ada di semua scene
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddItem(PotionType type, int amount = 1)
    {
        if (inventory.ContainsKey(type))
        {
            inventory[type] += amount;
        }
        else
        {
            inventory[type] = amount;
        }

        Debug.Log($"Added {amount} {type} to inventory. Total: {inventory[type]}");
    }

    public int GetItemCount(PotionType type)
    {
        return inventory.ContainsKey(type) ? inventory[type] : 0;
    }

    public void PrintAllInventory()
    {
        foreach (var item in inventory)
        {
            Debug.Log($"{item.Key}: {item.Value}");
        }
    }
}
