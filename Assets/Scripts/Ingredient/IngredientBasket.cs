using UnityEngine;

public class IngredientBasket : MonoBehaviour
{
    public PotionType allowedType;
    public GameObject itemPrefab;

    public void GiveItemToPlayer()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj == null) return;

        PlayerInventory playerInventory = playerObj.GetComponent<PlayerInventory>();

        if (playerInventory != null && !playerInventory.HasItem())
        {
            int available = InventoryManager.Instance.GetItemCount(allowedType);
            if (available > 0)
            {
                // Kurangi dari global inventory
                InventoryManager.Instance.AddItem(allowedType, -1);

                // Buat item di tangan player
                playerInventory.PickupItem(itemPrefab, allowedType);

                Debug.Log($"[Basket] Player ambil 1 {allowedType}. Sisa: {InventoryManager.Instance.GetItemCount(allowedType)}");
            }
            else
            {
                Debug.Log($"[Basket] Tidak ada {allowedType} di inventory.");
            }
        }
        else
        {
            Debug.Log("[Basket] Player sudah memegang sesuatu.");
        }
    }
}
