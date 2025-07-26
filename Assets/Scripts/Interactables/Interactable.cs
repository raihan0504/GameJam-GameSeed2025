using UnityEngine.Events;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [Header("Interaction Action")]
    public UnityEvent interactAction;
    private bool isPlayerInRange = false;

    public bool CanInteract()
    {
        return isPlayerInRange;
    }

    public void Interact()
    {
        if (isPlayerInRange)
        {
            Debug.Log("[Interactable] Interaksi sukses");
            interactAction?.Invoke();
        }
        else
        {
            Debug.Log("[Interactable] Gagal interaksi (tidak dalam jangkauan)");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = true;
            Debug.Log("Player dalam zona interaksi");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false;
            Debug.Log("Player keluar dari zona interaksi");
        }
    }
}