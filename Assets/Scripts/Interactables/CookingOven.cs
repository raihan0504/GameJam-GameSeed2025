using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookingOven : MonoBehaviour
{
    [Header("Resep Masakan")]
    public List<DishData> dishRecipes;

    [Header("Slider UI")]
    public GameObject sliderObject;         // UI slider container (panel biasa)
    public Slider cookingSlider;            // Komponen slider

    private float cookDuration = 3f;
    private float cookTimer = 0f;

    private List<PotionType> insertedIngredients = new();
    private bool isCooking = false;
    private bool isCookingComplete = false;

    private DishData currentDish;
    private GameObject spawnedDish;
    private Animator anim;

    private void Update()
    {
        anim.SetBool("isCook", isCooking);
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        insertedIngredients = new List<PotionType>();
        if (sliderObject != null) sliderObject.SetActive(false);
    }

    public void InsertIngredient(PotionType ingredient)
{
    if (isCooking || isCookingComplete)
    {
        Debug.Log("[Oven] Tidak bisa memasukkan bahan saat sedang memasak atau sudah selesai.");
        return;
    }

    insertedIngredients.Add(ingredient);
    Debug.Log($"[Oven] {ingredient} dimasukkan ({insertedIngredients.Count}/5)");

    if (insertedIngredients.Count > 5)
    {
        Debug.LogWarning("[Oven] Terlalu banyak bahan, pembersihan otomatis.");
        insertedIngredients.Clear();
        return;
    }

    if (insertedIngredients.Count >= 3)
    {
        TryStartCooking();
    }
}


    void TryStartCooking()
    {
        foreach (var dish in dishRecipes)
        {
            List<PotionType> temp = new(dish.requiredIngredients);

            foreach (var input in insertedIngredients)
            {
                if (temp.Contains(input))
                    temp.Remove(input);
            }

            if (temp.Count == 0)
            {
                // Bahan cocok, mulai memasak
                currentDish = dish;
                StartCoroutine(CookingCoroutine());
                return;
            }
        }

        Debug.Log("[Oven] Tidak cocok dengan resep apa pun.");
    }

    System.Collections.IEnumerator CookingCoroutine()
    {
        isCooking = true;
        cookTimer = 0f;

        if (sliderObject != null) sliderObject.SetActive(true);
        if (cookingSlider != null) cookingSlider.value = 0f;

        Debug.Log($"[Oven] Memasak {currentDish.dishType}...");

        while (cookTimer < cookDuration)
        {
            cookTimer += Time.deltaTime;
            if (cookingSlider != null)
                cookingSlider.value = cookTimer / cookDuration;

            yield return null;
        }

        spawnedDish = Instantiate(currentDish.dishPrefab, transform.position + Vector3.up * 1.5f, Quaternion.identity);
        isCooking = false;
        isCookingComplete = true;
        insertedIngredients.Clear();

        if (sliderObject != null) sliderObject.SetActive(false);

        Debug.Log($"[Oven] Masakan {currentDish.dishType} selesai!");
    }

    public void PickupDish(PlayerInventory playerInventory)
    {
        if (isCookingComplete && spawnedDish != null && !playerInventory.HasItem())
        {
            // Simpan dishType sebelum dish dihancurkan
            var dishType = currentDish.dishType;

            // Hapus dish dari scene
            Destroy(spawnedDish);

            // Berikan dish ke player
            playerInventory.PickupDish(currentDish.dishPrefab, dishType);

            // Log dan reset status oven
            Debug.Log($"[Oven] Pemain mengambil {dishType}");

            spawnedDish = null;
            isCookingComplete = false;
            currentDish = null;
        }
    }

    public void InteractWithOven()
    {
        var player = FindObjectOfType<PlayerInventory>();
        if (player == null) return;

        if (player.heldIngredient.HasValue)
        {
            InsertIngredient(player.heldIngredient.Value);
            player.DropItem();
        }
        else if (player.heldDish == null)
        {
            PickupDish(player);
        }
    }
}
