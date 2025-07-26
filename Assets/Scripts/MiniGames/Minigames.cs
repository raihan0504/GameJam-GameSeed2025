using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Minigames : MonoBehaviour
{
    public void OpenMiniGame()
    {
        Debug.Log("Welcome to candy crush");
        SceneManager.LoadSceneAsync(1);
    }
}
