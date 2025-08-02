using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
    [Header("Scene Transition")]
    public Animator transition;
    public string targetScene;               // nama scene tujuan
    public float transitionTime = 1f;

    [Header("Spawn Point")]
    public string spawnPointName;            // nama spawn point di scene tujuan

    private bool isTransitioning = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isTransitioning)
        {
            isTransitioning = true;
            StartCoroutine(LoadSceneWithSpawn());
        }
    }

    public void MiniGame()
    {
        isTransitioning = true;
        StartCoroutine(LoadSceneWithSpawn());
    }

    IEnumerator LoadSceneWithSpawn()
    {
        // Simpan spawn point untuk digunakan di scene tujuan
        SpawnManager.entranceName = spawnPointName;

        // Mainkan transisi jika ada animator
        if (transition != null)
        {
            transition.SetTrigger("Start");
            yield return new WaitForSeconds(transitionTime);
        }

        // Load scene secara async (bisa pakai SceneLoader juga)
        SceneManager.LoadSceneAsync(targetScene);
    }
}
