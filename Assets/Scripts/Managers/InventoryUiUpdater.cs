using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUiUpdater : MonoBehaviour
{
    [Header("Inventory UI Panel")]
    public GameObject inventoryPanel;            // Panel yang ditampilkan
    public Transform contentContainer;           // Tempat spawn item UI
    public GameObject itemPrefab;                // Prefab untuk setiap item

    [Header("Icon Lookup")]
    public PotionIcon[] potionIcons;             // Daftar icon untuk setiap type
    private Dictionary<PotionType, Sprite> iconLookup = new();

    private bool isOpen = false;

    public static InventoryUiUpdater instance;

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
        // Bangun lookup icon
        foreach (var icon in potionIcons)
        {
            if (!iconLookup.ContainsKey(icon.type))
                iconLookup.Add(icon.type, icon.icon);
        }

        inventoryPanel.SetActive(false); // Sembunyikan di awal
    }

    private void Update()
    {
        // Tekan tombol I untuk buka/tutup inventory
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleInventory();
        }
    }

    public void ToggleInventory()
    {
        isOpen = !isOpen;
        inventoryPanel.SetActive(isOpen);

        if (isOpen)
        {
            RefreshInventoryDisplay();
        }
    }

    public void RefreshInventoryDisplay()
    {
        // Bersihkan dulu isinya
        foreach (Transform child in contentContainer)
        {
            Destroy(child.gameObject);
        }

        // Tampilkan semua item yang dimiliki player
        foreach (PotionType type in System.Enum.GetValues(typeof(PotionType)))
        {
            int count = InventoryManager.Instance.GetItemCount(type);
            if (count > 0 && iconLookup.ContainsKey(type))
            {
                GameObject itemUI = Instantiate(itemPrefab, contentContainer);
                UIResultItem resultItem = itemUI.GetComponent<UIResultItem>();
                resultItem.Set(iconLookup[type], count);
            }
        }
    }
}
