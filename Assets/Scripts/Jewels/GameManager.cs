using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    //public string canvas = "Canvas";
    //private GameObject canvasUi;

    [Header("Timer")]
    public float timeLimit = 60f;
    private float timer;
    public TMP_Text timeText;
    public bool isGameEnded;

    [Header("UI Result Panel")]
    public GameObject resultPanel;
    public Transform resultContainer;
    public GameObject resultItemPrefab;
    public GameObject panelIngredient;

    [Header("Next Stage Button")]
    public Button nextStageButton; // tombol ini muncul saat game selesai

    [Header("Icon Lookup")]
    public PotionIcon[] potionIcons;
    private Dictionary<PotionType, Sprite> iconLookup = new();

    private void Awake()
    {
        //canvasUi = GameObject.Find("CanvasUI");

        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        timer = timeLimit;
        isGameEnded = false;

        resultPanel.SetActive(false);
        if (nextStageButton != null)
            nextStageButton.gameObject.SetActive(false); // tombol disembunyikan di awal

        foreach (var p in potionIcons)
        {
            if (!iconLookup.ContainsKey(p.type))
                iconLookup.Add(p.type, p.icon);
        }
    }

    private void Update()
    {
        if (isGameEnded) return;

        timer -= Time.deltaTime;
        timeText.text = "Time: " + Mathf.CeilToInt(timer).ToString();

        if (timer <= 0f)
        {
            timer = 0f;
            isGameEnded = true;
            EndGame();
        }
    }

    private void EndGame()
    {
        Debug.Log("Waktu habis! Menampilkan hasil...");

        resultPanel.SetActive(true);
        panelIngredient.SetActive(false);

        // Bersihkan isi sebelumnya jika ada
        foreach (Transform child in resultContainer)
        {
            Destroy(child.gameObject);
        }

        // Tampilkan setiap item yang dikumpulkan
        foreach (PotionType type in System.Enum.GetValues(typeof(PotionType)))
        {
            int count = InventoryManager.Instance.GetItemCount(type);
            if (count > 0 && iconLookup.ContainsKey(type))
            {
                GameObject itemObj = Instantiate(resultItemPrefab, resultContainer);
                UIResultItem resultItem = itemObj.GetComponent<UIResultItem>();
                resultItem.Set(iconLookup[type], count);
            }
        }

        if (nextStageButton != null)
            nextStageButton.gameObject.SetActive(true); // tampilkan tombol "Next Stage"
    }

    // Fungsi untuk tombol "Next Stage"
    public void OnNextStageButtonClicked()
    {
      
        SceneManager.LoadScene("Backyard stage 1-1");
        //canvasUi.SetActive(true);

    }
}

[System.Serializable]
public class PotionIcon
{
    public PotionType type;
    public Sprite icon;
}
