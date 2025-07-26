using UnityEngine;

public class MeshDeformer : MonoBehaviour
{
    public float force = 0.01f;
    public float deformRadius = 0.15f;

    private Mesh mesh;
    private Vector3[] originalVertices;
    private Vector3[] deformedVertices;

    void Start()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        if (meshFilter == null || meshFilter.sharedMesh == null)
        {
            Debug.LogError(" MeshFilter hoặc Mesh bị thiếu trên " + gameObject.name);
            return;
        }

        // tao ban sao de chinh sua sharedmesh
        mesh = Instantiate(meshFilter.sharedMesh);
        meshFilter.mesh = mesh;

        mesh.MarkDynamic();
        originalVertices = mesh.vertices;
        deformedVertices = mesh.vertices;
    }

    public void ApplyDeform(Vector3 hitPoint)
    {
        if (mesh == null)
        {
            Debug.LogError("❌ Mesh chưa được khởi tạo!");
            return;
        }

        for (int i = 0; i < deformedVertices.Length; i++)
        {
            Vector3 worldPos = transform.TransformPoint(originalVertices[i]);
            float distance = Vector3.Distance(worldPos, hitPoint);
            if (distance < deformRadius)
            {
                deformedVertices[i] += transform.InverseTransformDirection(Vector3.up) * force;
            }
        }

        mesh.vertices = deformedVertices;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
    }
}
