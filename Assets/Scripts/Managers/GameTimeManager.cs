using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameTimeManager : MonoBehaviour
{
    public static GameTimeManager instance;

    [Header("UI")]
    public TMP_Text timerText;
    public TMP_Text statusText;

    [Header("Order Manager")]
    public OrderManager orderManager;

    [Header("Waktu (detik)")]
    public float preOpenTime = 180f; // 3 menit before opening
    public float openTime = 180f;    // 3 menit shift

    private float currentTimer;
    private bool isOrderingPhase = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Agar tetap ada di semua scene
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        currentTimer = preOpenTime;
        statusText.text = "Before Opening";
        timerText.text = FormatTime(currentTimer);

        if (orderManager != null)
            orderManager.enabled = false;
    }

    private void Update()
    {
        if (currentTimer > 0)
        {
            currentTimer -= Time.deltaTime;
            UpdateTimerUI();
        }
        else
        {
            if (!isOrderingPhase)
            {
                StartOrderingPhase();
            }
            else
            {
                EndGame();
            }
        }
    }

    void StartOrderingPhase()
    {
        isOrderingPhase = true;
        currentTimer = openTime;
        statusText.text = "Orders Open";

        if (orderManager != null)
        {
            orderManager.enabled = true;
            orderManager.GenerateInitialOrders();
        }
    }

    void EndGame()
    {
        currentTimer = 0;
        statusText.text = "Shift Ended";
        Debug.Log("[TimeManager] Shift Ended");

        StartCoroutine(NextStageCoroutine());
        enabled = false; // Hentikan Update
    }

    IEnumerator NextStageCoroutine()
    {
        yield return new WaitForSeconds(2f);
        int nextSceneIndex = 3; // Ganti sesuai build index
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.LogWarning("Tidak ada scene berikutnya.");
        }
    }

    void UpdateTimerUI()
    {
        timerText.text = FormatTime(currentTimer);
    }

    string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        return $"{minutes}:{seconds:00}";
    }
}
