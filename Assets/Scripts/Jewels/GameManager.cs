using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Timer")]
    public float timeLimit = 60f;
    private float timer;
    public TMP_Text timeText;
    public bool isGameEnded;

    [Header("UI Result Panel")]
    public GameObject resultPanel;
    public Transform resultContainer; // Parent untuk prefab item
    public GameObject resultItemPrefab;
    public float resultDisplayDuration = 5f;
    public GameObject panelIngredient;

    [Header("Icon Lookup")]
    public PotionIcon[] potionIcons;
    private Dictionary<PotionType, Sprite> iconLookup = new();

    private void Awake()
    {
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

        // Bangun dictionary lookup icon
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

        StartCoroutine(ReturnToMainSceneAfterDelay());
    }

    IEnumerator ReturnToMainSceneAfterDelay()
    {
        yield return new WaitForSeconds(resultDisplayDuration);
        SceneManager.LoadScene("Backyard");
    }
}

[System.Serializable]
public class PotionIcon
{
    public PotionType type;
    public Sprite icon;
}
