using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class RopeMeshGenerator : MonoBehaviour
{
    public List<Transform> ropePoints;        // your rope cubes
    public int segmentsAround = 8;        // how many vertices around rope
    public float radius = 0.05f;
    public GameObject ropeRoot;
    Mesh mesh;

    void Start()
    {
        mesh = new Mesh();
        mesh.name = "RopeMesh";
        GetComponent<MeshFilter>().mesh = mesh;
    }

    void LateUpdate()
    {
        if (ropePoints.Count < 2) return;

        List<Vector3> verts = new List<Vector3>();
        List<int> tris = new List<int>();
        List<Vector3> normals = new List<Vector3>();
        List<Vector2> uvs = new List<Vector2>();

        // Build a ring for each rope point
        for (int i = 0; i < ropePoints.Count; i++)
        {

            //global position of the current point
            Vector3 pos = transform.InverseTransformPoint(ropePoints[i].position);

            // Find forward direction (toward next point or previous)
            Vector3 forward;
            if (i == ropePoints.Count - 1)
                forward = (ropePoints[i].position - ropePoints[i - 1].position).normalized;
            else
                forward = (ropePoints[i + 1].position - ropePoints[i].position).normalized;

            // Create an orientation basis (right & up vectors)
            Vector3 arbitrary = Vector3.up;
            if (Vector3.Dot(arbitrary, forward) > 0.9f) arbitrary = Vector3.right;
            Vector3 right = Vector3.Cross(forward, arbitrary).normalized;
            Vector3 up = Vector3.Cross(forward, right).normalized;

            float v = i / (float)(ropePoints.Count - 1);

            for (int j = 0; j < segmentsAround; j++)
            {
                float angle = (j / (float)segmentsAround) * Mathf.PI * 2f;
                Vector3 offset = Mathf.Cos(angle) * right * radius + Mathf.Sin(angle) * up * radius;
                verts.Add(pos + offset);
                normals.Add(offset.normalized);
                uvs.Add(new Vector2(j / (float)segmentsAround, v));
            }
        }

        int ringCount = ropePoints.Count;
        for (int i = 0; i < ringCount - 1; i++)
        {
            int ringStart = i * segmentsAround;
            int nextRingStart = (i + 1) * segmentsAround;

            for (int j = 0; j < segmentsAround; j++)
            {
                int next = (j + 1) % segmentsAround;

                tris.Add(ringStart + j);
                tris.Add(nextRingStart + j);
                tris.Add(nextRingStart + next);

                tris.Add(ringStart + j);
                tris.Add(nextRingStart + next);
                tris.Add(ringStart + next);
            }
        }
        for (int i = 0; i < ropePoints.Count - 1; i++)
        {
            Vector3 p1 = ropePoints[i].position;
            Vector3 p2 = ropePoints[i + 1].position;


            
            Destroy(ropePoints[i].gameObject.GetComponent<CapsuleCollider>());
            CapsuleCollider col = ropePoints[i].gameObject.AddComponent<CapsuleCollider>();
            
            // Calculate placement
            Vector3 mid = (p1 + p2) * 0.5f;
            Vector3 dir = (p2 - p1);
            float dist = dir.magnitude;

            ropePoints[i].transform.position = mid;
            ropePoints[i].transform.rotation = Quaternion.LookRotation(dir);

            col.direction = 2; // Z axis
            col.height = dist;
            col.radius = radius; // match your rope’s mesh radius
        }
        mesh.Clear();
        mesh.SetVertices(verts);
        mesh.SetTriangles(tris, 0);
        mesh.SetNormals(normals);
        mesh.SetUVs(0, uvs);
        mesh.RecalculateBounds();
    }
}
