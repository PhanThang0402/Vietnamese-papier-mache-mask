using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public float rotationSpeed = 100f;
    public PaperSelector paperSelector;
    public LayerMask maskLayer; 

    private bool isDragging = false;
    private Vector2 lastTouchPosition;

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                bool hitMask = Physics.Raycast(ray, Mathf.Infinity, maskLayer);

                if (!paperSelector.IsSelectingPaper())
                {
                    if (!hitMask)
                        isDragging = true;
                }
                else
                {
                    if (!hitMask)
                        paperSelector.ClearSelection(); //  Clear neu k bam trung khuon
                }

                lastTouchPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                isDragging = false;
            }

            if (isDragging && touch.phase == TouchPhase.Moved)
            {
                Vector2 delta = touch.position - lastTouchPosition;
                lastTouchPosition = touch.position;

                transform.Rotate(Vector3.up, -delta.x * rotationSpeed * Time.deltaTime * 0.1f, Space.World);
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                bool hitMask = Physics.Raycast(ray, Mathf.Infinity, maskLayer);

                if (!paperSelector.IsSelectingPaper())
                {
                    if (!hitMask)
                        isDragging = true;
                }
                else
                {
                    if (!hitMask)
                        paperSelector.ClearSelection();
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                isDragging = false;
            }

            if (isDragging)
            {
                float mouseX = Input.GetAxis("Mouse X");
                transform.Rotate(Vector3.up, -mouseX * rotationSpeed * Time.deltaTime, Space.World);
            }
        }
    }
}
