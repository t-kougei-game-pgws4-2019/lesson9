using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter),typeof(MeshRenderer))]
public class TrailController : MonoBehaviour
{
    private const int FRAME_MAX = 10;
    private List<Vector3> Points0 = new List<Vector3>();
    private List<Vector3> Points1 = new List<Vector3>();

    private Mesh mesh;

    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
    }

    // Update is called once per frame
    void Update()
    {
        if (FRAME_MAX <= Points0.Count)
        {
            Points0.RemoveAt(0);
            Points1.RemoveAt(0);
        }

        Points0.Add(transform.position);
        Points1.Add(transform.TransformPoint(new Vector3(0.0f, 1.0f, 0.0f)));

        if (2 <= Points0.Count)
        {
            CreateMesh();
        }
    }

    private void CreateMesh()
    {
        mesh.Clear();

        int n = Points0.Count;
        Vector3[] vertexArray = new Vector3[2 * n];
        Vector2[] uvArray = new Vector2[2 * n];
        int[] indexArray = new int[6 * (n - 1)];

        int idv = 0, idI = 0;
        float duv = 1.0f / ((float)n - 1.0f);

        for (int i = 0; i < n; i++)
        {
            vertexArray[idv + 0] = transform.InverseTransformPoint(Points0[i]);
            vertexArray[idv + 1] = transform.InverseTransformPoint(Points1[i]);

            uvArray[idv + 0].x =
                uvArray[idv + 1].x = 1.0f - duv * (float)i;
            uvArray[idv + 0].y = 0.0f;
            uvArray[idv + 1].y = 1.0f;

            if (i == n - 1) break;
            indexArray[idI + 0] = idv;
            indexArray[idI + 1] = idv + 1;
            indexArray[idI + 2] = idv + 2;
            indexArray[idI + 3] = idv + 2;
            indexArray[idI + 4] = idv + 1;
            indexArray[idI + 5] = idv + 3;

            idv += 2;
            idI += 6;
        }
        mesh.vertices = vertexArray;
        mesh.uv = uvArray;
        mesh.triangles = indexArray;

        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
    }

}
