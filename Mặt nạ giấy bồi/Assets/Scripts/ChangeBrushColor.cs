using UnityEngine;
using UnityEngine.UI;
using PaintIn3D;

public class ChangeBrushController : MonoBehaviour
{
    public CwPaintSphere paintSphere;
    public Slider brushSizeSlider; // Them slider UI

    void Start()
    {

        // 
        if (brushSizeSlider != null && paintSphere != null)
        {
            brushSizeSlider.value = paintSphere.Radius;
            brushSizeSlider.onValueChanged.AddListener(UpdateBrushSize);
        }
    }

    public void SetColorFromButton(Button button)
    {
        if (paintSphere != null && button != null)
        {
            Color buttonColor = button.GetComponent<Image>().color;
            paintSphere.Color = buttonColor;
        }
    }

   
    public void UpdateBrushSize(float value)
    {
        if (paintSphere != null)
        {
            paintSphere.Radius = value;
        }
    }
}
