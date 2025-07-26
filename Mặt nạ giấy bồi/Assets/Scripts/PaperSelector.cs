using UnityEngine;
using UnityEngine.UI;

public class PaperSelector : MonoBehaviour
{
    [Header("References")]
    public Button paper1Button;
    public Button paper2Button;

    public GameObject ob1;
    public GameObject ob2;

    private bool isPaper1Selected = false;
    private bool isPaper2Selected = false;

    void Start()
    {
        // bat tat loai giay khi chon
        paper1Button.onClick.AddListener(SelectPaper1);
        paper2Button.onClick.AddListener(SelectPaper2);
    }

    public void SelectPaper1()
    {
        if (isPaper1Selected)  // Neu pp1 dc chon, tat no
        {
            isPaper1Selected = false;
            ob1.SetActive(false);
            // Giu paper1Button ko sang
        }
        else
        {
            isPaper1Selected = true;
            ob1.SetActive(true);
            // Giu paper1Button sang (Duoc chon)
        }

        // Chi chon 1 nut
        if (isPaper1Selected)
        {
            isPaper2Selected = false;
            ob2.SetActive(false);
        }
    }

    public void SelectPaper2()
    {
        if (isPaper2Selected)  
        {
            isPaper2Selected = false;
            ob2.SetActive(false);
            
        }
        else
        {
            isPaper2Selected = true;
            ob2.SetActive(true);
            
        }

        // Chi 1 nut dc chon
        if (isPaper2Selected)
        {
            isPaper1Selected = false;
            ob1.SetActive(false);
        }
    }

    public bool IsSelectingPaper()
    {
        return isPaper1Selected || isPaper2Selected;
    }

    public void ClearSelection()
    {
        isPaper1Selected = false;
        isPaper2Selected = false;
        ob1.SetActive(false);
        ob2.SetActive(false);
    }
}
