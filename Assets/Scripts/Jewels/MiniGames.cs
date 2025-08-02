using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniGames : MonoBehaviour
{
    [Header("Scene Transition")]
    public Animator transition;
    public string targetScene;               // nama scene tujuan
    public float transitionTime = 1f;

    [Header("Spawn Point")]
    public string spawnPointName;            // nama spawn point di scene tujuan

    public void MiniGame()
    {
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
