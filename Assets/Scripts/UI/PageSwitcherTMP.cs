using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PageSwitcherTMP : MonoBehaviour
{
               
    public Button nextButton;              // Tombol panah kanan
    public Button previousButton;          // Tombol panah kiri

    private int currentPage = 1;
    private int minPage = 1;
    private int maxPage = 2;

    void Start()
    {
       

        nextButton.onClick.AddListener(() =>
        {
            if (currentPage < maxPage)
            {
                currentPage++;
                
            }
        });

        previousButton.onClick.AddListener(() =>
        {
            if (currentPage > minPage)
            {
                currentPage--;
                
            }
        });
    }


}