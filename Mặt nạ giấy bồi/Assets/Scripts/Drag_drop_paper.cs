using UnityEngine;

public class DragAndDropPaper : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;
    private Vector3 mousePosition;
    private Transform originalParent;
    private Renderer paperRenderer;
    public Material gluedMaterial; 
    public Transform halfSphere; 
    public float snapDistance = 0.2f; 

    void Start()
    {
        paperRenderer = GetComponent<Renderer>();
        originalParent = transform.parent; 
    }

    void Update()
    {
        if (isDragging)
        {
           
            mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f)); // Z = 10 để mảnh giấy luôn ở phía trước camera
            transform.position = mousePosition + offset; 
        }

       
        if (Vector3.Distance(transform.position, halfSphere.position) < snapDistance)
        {
           
            transform.position = halfSphere.position;
            transform.SetParent(halfSphere); 
            paperRenderer.material = gluedMaterial; 
        }
    }

    void OnMouseDown()
    {
        isDragging = true;
        offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f)); // Lưu lại vị trí cách chuột
    }

    void OnMouseUp()
    {
        isDragging = false;

        if (Vector3.Distance(transform.position, halfSphere.position) >= snapDistance)
        {
            transform.position = originalParent.position; 
            transform.SetParent(originalParent); 
            paperRenderer.material = originalParent.GetComponent<Renderer>().material; 
        }
    }
}
