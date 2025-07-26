using UnityEngine;

public class BrushController : MonoBehaviour
{
    [Header("References")]
    public Transform brushTip;
    public Camera mainCamera;
    public Transform brushModel; // Brush 3D model (parent object)

    [Header("Settings")]
    public float brushOffset = -0.02f;

    private bool isPainting = false;

    void Update()
    {
#if UNITY_EDITOR
        bool pressed = Input.GetMouseButton(0);
        Vector2 screenPos = Input.mousePosition;
#else
        bool pressed = Input.touchCount > 0 && Input.GetTouch(0).phase != TouchPhase.Ended;
        Vector2 screenPos = Input.touchCount > 0 ? Input.GetTouch(0).position : Vector2.zero;
#endif

        if (pressed)
        {
            if (!isPainting)
            {
                isPainting = true;
                brushModel.gameObject.SetActive(true);
            }

            UpdateBrushPosition(screenPos);
        }
        else if (isPainting)
        {
            isPainting = false;
            brushModel.gameObject.SetActive(false);
        }
    }

    private void UpdateBrushPosition(Vector2 screenPos)
    {
        Ray ray = mainCamera.ScreenPointToRay(screenPos);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            // Ngoi but tai diem va cham
            Vector3 tipOffset = brushTip.position - brushModel.position;
            Vector3 targetPos = hit.point - tipOffset + hit.normal * brushOffset;

            brushModel.position = targetPos;
            brushModel.rotation = Quaternion.LookRotation(hit.normal) * Quaternion.Euler(80, 180, 0);
        }
    }
}
