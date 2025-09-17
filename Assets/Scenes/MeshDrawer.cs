using UnityEngine;

public class MeshDrawer : MonoBehaviour
{
    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;

    public void DrawMesh(MeshData meshData, Texture2D texture)
    {
        Debug.Log("Creating mesh...");
        meshFilter.sharedMesh = meshData.CreateMesh();
        Debug.Log("Setting texture...");
        meshRenderer.sharedMaterial.mainTexture = texture;
        Debug.Log("Done Drawing Mesh");
    }
}