using UnityEngine;
using UnityEngine.SceneManagement;
public class MaskMakingUIManager : MonoBehaviour
{
    public GameObject maskMakingUI;
    public GameObject goToPaintButton;
    void Start()
    {
        goToPaintButton.SetActive(false);
    }
    public void OnFinishMaskMaking()
    {
       
        maskMakingUI.SetActive(false);

        
        goToPaintButton.SetActive(true);
    }
    public void GoToMenuScene()
    {
        PlayerPrefs.SetString("ReturnTo", "ChooseMask");
        PlayerPrefs.Save();
        SceneManager.LoadScene("All"); // 
    }
    public void GoToWukongColoringScene()
    {
        PlayerPrefs.SetString("ReturnTo", "ChooseMask");
        PlayerPrefs.Save();
        SceneManager.LoadScene("Wukong_colorring"); // 
    }
    public void GoToThi_NoColoringScene()
    {
        PlayerPrefs.SetString("ReturnTo", "ChooseMask");
        PlayerPrefs.Save();
        SceneManager.LoadScene("Thi_No_colorring"); // 
    }
    public void GoToChi_PheoColoringScene()
    {
        PlayerPrefs.SetString("ReturnTo", "ChooseMask");
        PlayerPrefs.Save();
        SceneManager.LoadScene("Chi_Pheo_colorring"); // 
    }

}
