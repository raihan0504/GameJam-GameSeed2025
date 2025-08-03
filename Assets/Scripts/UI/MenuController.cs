using UnityEngine;
using UnityEngine.Rendering;

public class MenuController : MonoBehaviour
{
    public GameObject mainPanel;      // UI utama
    public GameObject settingsPanel;  // UI pengaturan
    public GameObject panelTas;
    public GameObject page1;
    public GameObject page2;

    public void OpenSettings()
    {
        mainPanel.SetActive(false);      // Sembunyikan menu utama
        settingsPanel.SetActive(true);   // Tampilkan panel pengaturan
        panelTas.SetActive(false);
    }
    public void OpenTas()
    {
        mainPanel.SetActive(false);
        settingsPanel.SetActive(false);
        panelTas.SetActive(true);
    }
    public void OpenHelp()
    {
        mainPanel.SetActive(true);
        settingsPanel.SetActive(false);
        panelTas.SetActive(false);
    }
    public void Page1()
    {
        page1.SetActive(true);
        page2.SetActive(false);
    }
    public void Page2()
    {
        page2.SetActive(true);
        page1.SetActive(false);
    }
}

