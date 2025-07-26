using UnityEngine;
using UnityEngine.SceneManagement;

public class ARMaskUIManager : MonoBehaviour
{
    public GameObject arMask; //

    void Start()
    {
        
        if (ColoringUIManager.paintedMaskToTransfer != null)
        {
            // copy painted mask, gan lam child cua arMask
            GameObject paintedMaskClone = Instantiate(ColoringUIManager.paintedMaskToTransfer);
            paintedMaskClone.transform.SetParent(arMask.transform);  

            // Tim mask trong arMask de lay pos,rotation,scale
            Transform maskTransform = arMask.transform.Find("mask"); 

            if (maskTransform != null)
            {
                //Dieu chinh voi pos,rotation,scale cua mask
                paintedMaskClone.transform.localPosition = maskTransform.localPosition;  
                //paintedMaskClone.transform.localRotation = maskTransform.localRotation;  
                paintedMaskClone.transform.localScale = maskTransform.localScale;

                string originalName = ColoringUIManager.paintedMaskToTransfer.name;

                if (originalName == "Thi_no_final" || originalName == "Chi_pheo_final")
                {
                    paintedMaskClone.transform.rotation = ColoringUIManager.paintedMaskToTransfer.transform.rotation;

                    // Chinh sai so toa do(Loi 3D)
                    if (originalName == "Chi_pheo_final")
                    {
                        Vector3 adjustedPos = paintedMaskClone.transform.localPosition;
                        adjustedPos.z = -0.0295f;
                        paintedMaskClone.transform.localPosition = adjustedPos;
                    }
                    else if (originalName == "Thi_no_final")
                    {
                        Vector3 adjustedPos = paintedMaskClone.transform.localPosition;
                        adjustedPos.x = -0.0943f;
                        adjustedPos.y = 0.0737f;
                        paintedMaskClone.transform.localPosition = adjustedPos;
                    }


                }
                else
                {
                    paintedMaskClone.transform.localRotation = maskTransform.localRotation;
                }
            }


            Debug.Log("gan thanh cong");
        }
        else
        {
            Debug.LogError("Khong tim thay painted mask");
        }
    }
    public void GoToMenuScene()
    {
        PlayerPrefs.SetString("ReturnTo", "ChooseMask");
        PlayerPrefs.Save();
        SceneManager.LoadScene("All"); // 
        Destroy(ColoringUIManager.paintedMaskToTransfer);
        ColoringUIManager.paintedMaskToTransfer = null;
    }
}
