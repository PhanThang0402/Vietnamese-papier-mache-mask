using UnityEngine;
using UnityEngine.UI;

public class IntroPageController : MonoBehaviour
{
    public GameObject[] pages; 
    public Sprite activeDot, inactiveDot;
    public GameObject maskSelectionScreen; 

    private int currentPage = 0;

    void Start()
    {
        UpdateUI();
    }

    public void NextPage()
    {
        if (currentPage < pages.Length - 1)
        {
            currentPage++;
            UpdateUI();
        }
        else
        {
            GoToMaskSelection();
        }
    }

    public void SkipIntro()
    {
        GoToMaskSelection();
    }

    void GoToMaskSelection()
    {
        this.gameObject.SetActive(false);
        maskSelectionScreen.SetActive(true);
    }

    void UpdateUI()
    {
       
        for (int i = 0; i < pages.Length; i++)
        {
            pages[i].SetActive(i == currentPage);
        }

        
    }
}
