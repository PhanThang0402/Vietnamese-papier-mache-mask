using UnityEngine;

public class ChangeMaterial : MonoBehaviour
{
    public Material newMaterial;

    void Start()
    {
        // Lay MeshRenderer tu GameObject
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();

        // thay material
        if (meshRenderer != null && newMaterial != null)
        {
            meshRenderer.material = newMaterial;
        }
        else
        {
            Debug.LogError(" loi chua gan hoac ko tim thay");
        }
    }
}
