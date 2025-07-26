using UnityEngine;

public class MarkerTouchController : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject paintObject; 

    void Update()
    {
        Vector2 inputPosition = Vector2.zero;
        bool isTouching = false;

#if UNITY_EDITOR
        if (Input.GetMouseButton(0))
        {
            inputPosition = Input.mousePosition;
            isTouching = true;
        }
#else
        if (Input.touchCount > 0)
        {
            inputPosition = Input.GetTouch(0).position;
            isTouching = true;
        }
#endif

        if (isTouching)
        {
            Ray ray = mainCamera.ScreenPointToRay(inputPosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                // Đat marker tai diem va cham
                transform.position = hit.point;

                // Quay marker vuong goc be mat
                transform.rotation = Quaternion.LookRotation(hit.normal);

                // bat paint khi va cham
                if (paintObject != null) paintObject.SetActive(true);
            }
        }
        else
        {
            if (paintObject != null) paintObject.SetActive(false);
        }
    }
}
