using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public string handSlotName = "HandSlot"; // nama child transform

    private Transform handSlot;
    public Animator anim;

    private GameObject heldObject;
    public PotionType? heldIngredient = null;
    public DishType? heldDish = null;

    private void Awake()
    {
        handSlot = transform.Find(handSlotName);
        if (handSlot == null)
        {
            Debug.LogError($"[PlayerInventory] HandSlot '{handSlotName}' tidak ditemukan!");
        }
    }

    public bool HasItem()
    {
        return heldObject != null;
    }

    public void PickupItem(GameObject prefab, PotionType type)
    {
        if (HasItem() || handSlot == null) return;

        heldObject = Instantiate(prefab, handSlot.position, Quaternion.identity);
        heldObject.transform.SetParent(handSlot);
        heldIngredient = type;
        heldDish = null;

        anim?.SetBool("isHoldItem", true);
    }

    public void PickupDish(GameObject prefab, DishType dish)
    {
        if (HasItem() || handSlot == null) return;

        heldObject = Instantiate(prefab, handSlot.position, Quaternion.identity);
        heldObject.transform.SetParent(handSlot);
        heldDish = dish;
        heldIngredient = null;

        anim?.SetBool("isHoldItem", true);
    }

    public void DropItem()
    {
        if (!HasItem()) return;

        Destroy(heldObject);
        heldObject = null;
        heldDish = null;
        heldIngredient = null;

        anim?.SetBool("isHoldItem", false);
    }
}
