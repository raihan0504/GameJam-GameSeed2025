using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionsMenuController : MonoBehaviour
{
    public Slider musicSlider;
    public TMP_Text displayModeText; // text di UI yang menunjukkan mode layar

    private bool isFullScreen;

    void Start()
    {
        // --- Musik ---
        float savedVolume = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        musicSlider.value = savedVolume;
        AudioListener.volume = savedVolume;
        musicSlider.onValueChanged.AddListener(SetMusicVolume);

        // --- Display ---
        isFullScreen = PlayerPrefs.GetInt("FullScreen", 1) == 1;
        ApplyFullScreenSetting();
    }

    public void SetMusicVolume(float volume)
    {
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public void ToggleDisplayMode()
    {
        isFullScreen = !isFullScreen;
        ApplyFullScreenSetting();
        PlayerPrefs.SetInt("FullScreen", isFullScreen ? 1 : 0);
    }

    private void ApplyFullScreenSetting()
    {
        Screen.fullScreen = isFullScreen;
        displayModeText.text = isFullScreen ? "Full Screen" : "Windowed";
    }
}
