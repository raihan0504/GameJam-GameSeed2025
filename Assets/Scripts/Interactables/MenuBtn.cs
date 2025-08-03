using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuBtn : MonoBehaviour
{
    public GameObject startMenu;
    public GameObject settingMenu;
    public GameObject btnMenu;


    public void StartMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }

    public void SettingMenu()
    {
        settingMenu.SetActive(true);
    }

    public void stopSetting()
    {
        settingMenu.SetActive(false);
    }
}
