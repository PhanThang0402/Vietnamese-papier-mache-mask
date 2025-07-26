using UnityEngine;
using UnityEngine.SceneManagement;

public class ColoringUIManager : MonoBehaviour
{
    // Luu mat na da to mau
    public static GameObject paintedMaskToTransfer;

    // Mat na da to mau
    public GameObject paintedMask;

    // load AR scene
    public void TryOnMask()
    {
       
        paintedMaskToTransfer = paintedMask;
       
        DontDestroyOnLoad(paintedMask);
        SceneManager.LoadScene("Wukong_AR");
    }
    public void GoToMenuScene()
    {
        PlayerPrefs.SetString("ReturnTo", "ChooseMask");
        PlayerPrefs.Save();
        SceneManager.LoadScene("All"); // 
    }
}





