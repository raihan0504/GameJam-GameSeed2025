using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIResultItem : MonoBehaviour
{
    public Image iconImage;
    public TMP_Text amountText;

    public void Set(Sprite icon, int amount)
    {
        iconImage.sprite = icon;
        amountText.text = "x" + amount.ToString();
    }
}
