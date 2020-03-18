using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseGen : MonoBehaviour {

    public float Power = 3;
    public float Scale = 1;
    public float TimeScale = 1;

    private float xOffSet;
    private float yOffSet;

    private MeshFilter mf;

    void Start()
    {
        mf = GetComponent<MeshFilter>();
        WaveGeneration();
    }

    void Update()
    {
        WaveGeneration();
        xOffSet += Time.deltaTime * TimeScale;
        yOffSet += Time.deltaTime * TimeScale;
    }

    void WaveGeneration()
    {
        Vector3[] vertices = mf.mesh.vertices;

        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i].y = CalculateHeight(vertices[i].x, vertices[i].z) * Power;
        }

        mf.mesh.vertices = vertices;
    }

    float CalculateHeight(float x, float y)
    {
        float xCord = x * Scale + xOffSet;
        float yCord = y * Scale + yOffSet;

        return Mathf.PerlinNoise(xCord, yCord);
    }
}
