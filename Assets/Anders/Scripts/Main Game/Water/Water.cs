using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour {

    public float Size = 1;
    public int GridSize = 16;

    private MeshFilter filter;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Island"))
        {
            
                foreach(Vector3 v in gameObject.GetComponent<Mesh>().vertices)
            {
                new Vector3(0, 0, 0);
            }
        }
        else {}
    }
    private void Start()
    {
        filter = GetComponent<MeshFilter>();
        filter.mesh = GenerateMesh();
    }

    private Mesh GenerateMesh()
    {
        Mesh m = new Mesh();

        var verticies = new List<Vector3>();
        var Normals = new List<Vector3>();
        var UVs = new List<Vector2>();

        for (int x = 0; x < GridSize + 1; x++)
        {
            for (int y = 0; y < GridSize + 1; y++)
            {
                verticies.Add(new Vector3(-Size * 0.5f + Size * (x / ((float)GridSize)), 0, -Size * 0.5f + Size * (y / ((float)GridSize))));
                Normals.Add(Vector3.up);
                UVs.Add(new Vector2(x / (float)GridSize, y / (float)GridSize));
            }
        }

        var Triangles = new List<int>();
        var VertCount = GridSize + 1;

        for (int i = 0; i < VertCount * VertCount - VertCount; i++)
        {
            if ((i + 1) % VertCount == 0)
            {
                continue;
            }
            Triangles.AddRange(new List<int>()
            {
                i+1+VertCount, i+VertCount, i,
                i, i+1, i+VertCount+1
            });

        }

        m.SetVertices(verticies);
        m.SetNormals(Normals);
        m.SetUVs(0, UVs);
        m.SetTriangles(Triangles, 0);

        return m;
    } 
}
