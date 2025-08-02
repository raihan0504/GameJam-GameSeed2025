using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSpawners : MonoBehaviour
{
    public GameObject playerPrefab;

    void Start()
    {
        string entrance = SpawnManager.entranceName;
        Debug.Log("Entrance Name: " + entrance);

        Transform spawnPoint = GameObject.Find(entrance)?.transform;

        if (spawnPoint != null)
        {
            Debug.Log("Spawn point found: " + spawnPoint.name);
            Instantiate(playerPrefab, spawnPoint.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Spawn point NOT FOUND: " + entrance);
            Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        }
    }
}
