using UnityEngine;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    public TMP_Text timerText;       // Hubungkan ke teks yang menampilkan "3:00"
    public float timeRemaining = 180f;  // 3 menit = 180 detik
    private bool isRunning = true;

    void Update()
    {
        if (isRunning && timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTimerUI();
        }
        else if (isRunning && timeRemaining <= 0)
        {
            timeRemaining = 0;
            isRunning = false;
            UpdateTimerUI();
            // TODO: Lakukan sesuatu saat waktu habis
            Debug.Log("Waktu Habis!");
        }
    }

    void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60f);
        int seconds = Mathf.FloorToInt(timeRemaining % 60f);
        timerText.text = string.Format("{0}:{1:00}", minutes, seconds);
    }
}
