using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class render : MonoBehaviour
{








    private Vector3[] newVertices;

    private Material material;

    private int[] newTriangles = new int[] { 0, 1, 2, 0, 2, 3 };


    // Start is called before the first frame update
    void Awake()
    {
        Mesh mesh = new Mesh();
        Vector3 V1 = new Vector3(0, 0, 0);
        Vector3 V2 = new Vector3(0, 100, 0);
        Vector3 V3 = new Vector3(100, 100, 0);
        Vector3 V4 = new Vector3(100, 0, 0);
        newVertices = new Vector3[] { V1, V2, V3, V4 };
        mesh.vertices = newVertices;
        mesh.triangles = newTriangles;
        CanvasRenderer rend = GetComponent<CanvasRenderer>();
        rend.SetMaterial(material, 1);
        rend.SetMesh(mesh);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
