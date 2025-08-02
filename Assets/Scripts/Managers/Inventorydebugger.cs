using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventorydebugger : MonoBehaviour
{
    private Button debugButton;

    void Awake()
    {
        debugButton = GetComponent<Button>();
        debugButton.onClick.AddListener(PrintInventory);
    }

    void PrintInventory()
    {
        InventoryManager.Instance.PrintAllInventory();
    }
}
