using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static string entranceName;
    public GameObject playerPrefab;

    void Start()
    {
        GameObject spawnPoint = GameObject.Find(entranceName);
        if (spawnPoint != null && playerPrefab != null)
        {
            Instantiate(playerPrefab, spawnPoint.transform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Spawn point or player prefab not found");
        }
    }
}
