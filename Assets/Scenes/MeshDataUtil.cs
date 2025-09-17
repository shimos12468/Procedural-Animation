using UnityEngine;

public class Triangle
{
    public Vector3 vertex1;
    public Vector3 vertex2;
    public Vector3 vertex3;

    public Triangle(Vector3 v1, Vector3 v2, Vector3 v3)
    {
        vertex1 = v1;
        vertex2 = v2;
        vertex3 = v3;
    }

    public override string ToString()
    {
        return string.Format("{0,-40}{1,-40}{2,-40}", vertex1, vertex2, vertex3);
    }
}

public class MeshData
{
    public Vector3[] vertices;
    public int[] triangleIdxs;
    public Vector2[] uvs;

    private int lastVertexIdx = -1;

    public MeshData()
    {
        Reset();
    }

    public void Reset()
    {
        vertices = new Vector3[0];
        triangleIdxs = new int[0];
        uvs = new Vector2[0];
        lastVertexIdx = -1;
    }

    public int AddVertex(Vector3 v, Vector2 uv)
    {
        System.Array.Resize(ref vertices, vertices.Length + 1);
        System.Array.Resize(ref uvs, uvs.Length + 1);

        vertices[lastVertexIdx + 1] = v;
        uvs[lastVertexIdx + 1] = uv;

        lastVertexIdx++;
        return lastVertexIdx;
    }

    public void AddTriangleIdxs(int i1, int i2, int i3)
    {
        int oldLength = triangleIdxs.Length;
        System.Array.Resize(ref triangleIdxs, oldLength + 3);

        triangleIdxs[oldLength] = i1;
        triangleIdxs[oldLength + 1] = i2;
        triangleIdxs[oldLength + 2] = i3;
    }

    public Triangle[] Triangles
    {
        get
        {
            int triangleCount = triangleIdxs.Length / 3;
            Triangle[] triangleArray = new Triangle[triangleCount];
            for (int i = 0; i < triangleCount; i++)
            {
                int idx = i * 3;
                triangleArray[i] = new Triangle(vertices[triangleIdxs[idx]], vertices[triangleIdxs[idx + 1]], vertices[triangleIdxs[idx + 2]]);
            }
            return triangleArray;
        }
    }

    public string TrianglesToString()
    {
        string result = "";
        foreach (Triangle t in Triangles)
        {
            result += t.ToString() + "\n";
        }
        return result;
    }

    public Mesh CreateMesh()
    {
        Mesh mesh = new Mesh
        {
            vertices = vertices,
            triangles = triangleIdxs,
            uv = uvs
        };
        mesh.RecalculateNormals();
        return mesh;
    }
}

public static class DemoDataUtil
{
    public static MeshData GetDemoData()
    {
        MeshData meshData = new MeshData();

        var v = new Vector3(0, 0, 0);
        var uv = new Vector2(0, 0);
        var vIdx1 = meshData.AddVertex(v, uv);

        v = new Vector3(1, 0, 0);
        uv = new Vector2(1, 0);
        var vIdx2 = meshData.AddVertex(v, uv);

        v = new Vector3(1, 1, 0);
        uv = new Vector2(1, 1);
        var vIdx3 = meshData.AddVertex(v, uv);



        meshData.AddTriangleIdxs(vIdx1, vIdx3, vIdx2); //CLOCKWISE


        v = new Vector3(0, 0, 0);
        uv = new Vector2(0, 0);
        var vIdx4 = meshData.AddVertex(v, uv);

        v = new Vector3(0, 1, 0);
        uv = new Vector2(0, 1);
        var vIdx5 = meshData.AddVertex(v, uv);

        v = new Vector3(1, 1, 0);
        uv = new Vector2(1, 1);
        var vIdx6 = meshData.AddVertex(v, uv);

        meshData.AddTriangleIdxs(vIdx4, vIdx5, vIdx6); //CLOCKWISE

        return meshData;
    }
}