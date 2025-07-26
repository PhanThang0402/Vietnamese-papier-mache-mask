using UnityEngine;
using PaintIn3D;

public class SimulatedBrush : MonoBehaviour
{
    public Camera cam;
    public LayerMask paintableLayer;
    public float heightOffset = 0.1f; // Cao hon be mat 1 chut

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 100f, paintableLayer))
            {
                Vector3 targetPos = hit.point + hit.normal * heightOffset;
                transform.position = targetPos;
                transform.rotation = Quaternion.LookRotation(-hit.normal);
            }
        }
    }
}
