using UnityEngine;
using PaintIn3D;
using CW.Common;
using PaintCore;
public class ChangeColorPen : MonoBehaviour
{
    public CwPaintSphere paintSphere; //Tro vao but hien tai

    // Doi mau
    public void SetColor(Color color)
    {
       
        {
            paintSphere.Color = color;
        }
    }
}
