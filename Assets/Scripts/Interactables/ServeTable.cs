using UnityEngine;

public class ServeTable : MonoBehaviour
{
    public DishType currentRequest;
    public int score = 0;

    private void Start()
    {
        SetRandomRequest();
    }

    public void Serve(PlayerInventory player)
    {
        if (!player.HasItem() || player.heldDish == null)
        {
            Debug.Log("[ServeTable] Tidak membawa dish");
            return;
        }

        if (player.heldDish == currentRequest)
        {
            score += 10;
            Debug.Log($"[ServeTable] BENAR! Skor: {score}");
        }
        else
        {
            score -= 5;
            Debug.Log($"[ServeTable] SALAH! Skor: {score}");
        }

        player.DropItem();
        SetRandomRequest();
    }

    void SetRandomRequest()
    {
        var dishes = System.Enum.GetValues(typeof(DishType));
        currentRequest = (DishType)dishes.GetValue(Random.Range(0, dishes.Length));
        Debug.Log($"[ServeTable] Permintaan baru: {currentRequest}");
    }

    public void InteractWithTable()
    {
        var player = FindObjectOfType<PlayerInventory>();
        if (player != null)
        {
            Serve(player);
        }
    }
}
