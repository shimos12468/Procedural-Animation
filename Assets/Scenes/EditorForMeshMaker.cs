using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MeshMaker))]
public class EditorForMeshMaker : Editor
{
    public override void OnInspectorGUI()
    {
        MeshMaker maker = (MeshMaker)target;

        if (DrawDefaultInspector())
        {
            if (maker.autoUpdate)
            {
                Debug.Log("Params updated. Re-generating mesh...");
                maker.MakeMesh();
            }
        }

        if (GUILayout.Button("Generate Mesh"))
        {
            Debug.Log("Generate Mesh was clicked...");
            maker.MakeMesh();
        }
    }
}