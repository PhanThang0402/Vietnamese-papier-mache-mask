using UnityEngine;
using UnityEngine.UI;

public class PaperPlacement : MonoBehaviour
{
    public GameObject paperPrefabLayer1; 
    public GameObject paperPrefabLayer2;  
    public Transform paperParent;
    public LayerMask maskLayer;
    public Button nextLayerButton; 

    private bool isFirstLayerComplete = false; //
    private float secondLayerOffset = 0.05f; // do cao nang len cho lop 2

    void Start()
    {
        nextLayerButton.onClick.AddListener(CompleteFirstLayer); 
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100f, maskLayer))
            {
                if (hit.collider.CompareTag("Mask") || (isFirstLayerComplete && hit.collider.CompareTag("Paper")))
                {
                    PlacePaper(hit.point, hit.normal);
                }
            }
        }
    }

    void PlacePaper(Vector3 position, Vector3 normal)
    {
        // chon prefab tuy lop
        GameObject prefabToUse = isFirstLayerComplete ? paperPrefabLayer2 : paperPrefabLayer1;

        GameObject paper = Instantiate(prefabToUse, position, Quaternion.identity, paperParent);
        paper.transform.rotation = Quaternion.FromToRotation(Vector3.up, normal);

        // Dieu chinh theo be mat ben duoi
        AdjustPaperToSurface(paper);

        // Lop 2, nang len 1 doan so voi lop 1
        if (isFirstLayerComplete)
        {
            paper.transform.position += normal * secondLayerOffset;
        }

        paper.tag = "Paper";
    }

    void AdjustPaperToSurface(GameObject paper)
    {
        Mesh mesh = paper.GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;
        Vector3[] newVertices = new Vector3[vertices.Length];

        float offset = 0.03f; // Do cao de tranh giay cat vao khuon

        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 worldVertex = paper.transform.TransformPoint(vertices[i]);
            RaycastHit hit;

            if (Physics.Raycast(worldVertex + Vector3.up * 0.1f, -Vector3.up, out hit, 1f, maskLayer))
            {
                Vector3 newPosition = hit.point + hit.normal * offset;
                newVertices[i] = paper.transform.InverseTransformPoint(newPosition);
            }
            else
            {
                newVertices[i] = vertices[i];
            }
        }

        mesh.vertices = newVertices;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
    }

    public void CompleteFirstLayer()
    {
        isFirstLayerComplete = true;
        nextLayerButton.interactable = false; // tat nut sau khi bam
    }
}
