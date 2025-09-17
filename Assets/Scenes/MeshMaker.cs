using System;
using UnityEngine;

public class MeshMaker : MonoBehaviour
{
    public bool autoUpdate;

    [Range(3, 40)]
    public int numSides;

    [Min(0.01f)]
    public float length;

    [Min(0.01f)]
    public float polygonSideLength;
    [SerializeField] MeshDrawer drawer;

    [SerializeField] MeshDrawer drawer2;
    [SerializeField] MeshDrawer drawer3;
    [SerializeField] MeshDrawer drawer4;
    [SerializeField] MeshDrawer drawer5;
    public void MakeMesh()
    {
        Debug.Log("GenerateMesh invoked...");

        Debug.Log("Getting texture...");
        var texture = GetTexture();

        Debug.Log("Getting mesh...");
        var meshData = PolygonalCylinderMeshMaker.GenerateMeshData(numSides, length, polygonSideLength);


        Debug.Log("Getting mesh...");
        var meshData2 = PolygonalCylinderMeshMaker.GenerateMeshData(numSides, length, polygonSideLength);

        var meshData3 = PolygonalCylinderMeshMaker.GenerateMeshData(numSides, length, polygonSideLength);


        var meshData4 = PolygonalCylinderMeshMaker.GenerateMeshData(numSides, length, polygonSideLength);

        var meshData5 = PolygonalCylinderMeshMaker.GenerateMeshData(numSides, length, polygonSideLength);

        for (int i = 0; i < meshData2.vertices.Length; i++)
        {
            
            meshData2.vertices[i]= transform.InverseTransformPoint(meshData.vertices[i]);

            meshData2.vertices[i] = meshData.vertices[i]+Vector3.forward*length;
        }


        for (int i = 0; i < meshData2.vertices.Length; i++)
        {

            meshData3.vertices[i] = transform.InverseTransformPoint(meshData2.vertices[i]);

            meshData3.vertices[i] = meshData2.vertices[i] + Vector3.forward * length;
        }



        for (int i = 0; i < meshData2.vertices.Length; i++)
        {

            meshData4.vertices[i] = transform.InverseTransformPoint(meshData3.vertices[i]);

            meshData4.vertices[i] = meshData3.vertices[i] + Vector3.forward * length;
        }

        for (int i = 0; i < meshData2.vertices.Length; i++)
        {

            meshData5.vertices[i] = transform.InverseTransformPoint(meshData4.vertices[i]);

            meshData5.vertices[i] = meshData4.vertices[i] + Vector3.forward * length;
        }

        Debug.Log("Drawing mesh...");
        drawer.DrawMesh(meshData, texture);
        drawer2.DrawMesh(meshData2, texture);
        drawer3.DrawMesh(meshData3, texture);
        drawer4.DrawMesh(meshData4, texture);
        drawer5.DrawMesh(meshData5, texture);
        Debug.Log("Mesh generation complete");
    }

    private static Texture2D GetTexture()
    {
        Texture2D texture = new Texture2D(1, 1);
        return texture;
    }


    




}