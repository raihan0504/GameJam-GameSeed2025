using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Kalau pakai TextMeshPro
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameTimeManager : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text timerText;
    public TMP_Text statusText; // Misalnya: "Before Opening", "Orders Open"

    [Header("Order Manager")]
    public OrderManager orderManager; // Drag komponen OrderManager ke sini

    [Header("Waktu dalam detik")]
    public float preOpenTime = 180f; // 3 menit sebelum buka
    public float openTime = 180f;    // 3 menit buka

    private float timer;
    private bool isOrderingPhase = false;

    private void Start()
    {
        timer = preOpenTime + openTime;
        if (orderManager != null)
            orderManager.enabled = false; // Matikan order saat pre-opening
    }

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            UpdateTimerUI();

            if (!isOrderingPhase && timer <= openTime)
            {
                StartOrderingPhase();
            }
        }
        else
        {
            EndGame();
        }
    }

    void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(timer / 60f);
        int seconds = Mathf.FloorToInt(timer % 60f);

        if (timer > openTime)
        {
            statusText.text = "Before Opening";
        }
        else
        {
            statusText.text = "Orders Open";
        }

        timerText.text = $"{minutes}:{seconds:00}";
    }

    void StartOrderingPhase()
    {
        isOrderingPhase = true;
        Debug.Log("[TimeManager] Order Phase Started");

        if (orderManager != null)
        {
            orderManager.enabled = true;
            orderManager.GenerateInitialOrders();
        }
    }

    void EndGame()
    {
        timer = 0;
        Debug.Log("[TimeManager] Shift Ended");
        statusText.text = "Shift Ended";

        StartCoroutine(NextStageCoroutine());
    }

    IEnumerator NextStageCoroutine()
    {
        yield return new WaitForSeconds(2f); // Tunggu sebentar (opsional)


        int nextSceneIndex = 3;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.LogWarning("Tidak ada scene berikutnya. Tetap di stage ini.");
        }
    }

}
