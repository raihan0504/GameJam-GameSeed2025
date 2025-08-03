using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public Slider musicSlider;

    void Start()
    {
        // Pastikan slider tidak null sebelum digunakan
        if (musicSlider == null)
        {
            Debug.LogError("MusicSlider belum di-assign di Inspector!");
            return;
        }

        // Ambil nilai dari PlayerPrefs (jika ada), atau pakai default
        float savedVolume = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        musicSlider.value = savedVolume;
        AudioListener.volume = savedVolume;

        // Pasang listener untuk slider
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
    }

    public void SetMusicVolume(float volume)
    {
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }
}
