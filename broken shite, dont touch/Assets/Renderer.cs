using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;


// robbed from https://www.youtube.com/watch?v=YG-gIX_OvSE, thanks Kevin for your donation of all your code :D

public class Renderer : MonoBehaviour
{

    Mesh mesh;

    public Vector3[] polygonPoints;
    public Vector2[] uv;
    public int[] polygonTriangles;

    public bool isFilled;
    public int polygonSides;
    public float polygonRadius;
    public float polygonCentreRadius;

    [SerializeField]bool changingShape;
    public bool auto = false;

    public Material sharedMaterial;
    public Material material;
    GameObject parent;
    

    // Start is called before the first frame update
    void Awake()
    {
        sharedMaterial= GetComponent<Material>();
        material=sharedMaterial;
        mesh = new Mesh();
        this.GetComponent<MeshFilter>().mesh = mesh;
        parent = gameObject.transform.parent.gameObject;
    }

    public void Fill(int shape = 0, bool randomizeRotation = true)
    {

        if (shape == 0)
        {
            if (isFilled)
            {
                DrawFilled(polygonSides, polygonRadius);
            }
            else
            {
                DrawHollow(polygonSides, polygonRadius, polygonCentreRadius);
            }
        }
        else
        {
            Vector3[] vertices;
            int[] triangles;
            float radius;
            List<Vector3> points = new List<Vector3>();
            switch (shape)
            {
                case 0:
                    //impossible lmao
                    break;

                case 10:
                    //square

                    vertices = new Vector3[]
                    { 
                        new Vector3(0, 0), 
                        new Vector3(1, 0), 
                        new Vector3(0, 1), 
                        new Vector3(1, 1)
                    }; //no fucking clue

                    uv = new Vector2[]
                    {
                        new Vector2(0, 0),
                        new Vector2(1, 0),
                        new Vector2(0, 1),
                        new Vector2(1, 1)
                    };

                    triangles = new int[]
                    {
                        0, 1, 2,
                        3, 1, 2
                    };
                    NewMesh(vertices, uv, triangles);



                    #region graveyard
                    //int triangleAmount = polygonPoints.Length - 2;
                    //List<int> _newTriangles = new List<int>();
                    //for (int i = 0; i < triangleAmount; i++)
                    //{
                    //    _newTriangles.Add(0);
                    //    _newTriangles.Add(i + 2);
                    //    _newTriangles.Add(i + 1);
                    //}
                    //polygonTriangles = _newTriangles.ToArray;


                    //int triangleAmount = points.Length - 2;
                    //List<int> newTriangles = new List<int>();
                    //for (int i = 0; i < triangleAmount; i++)
                    //{
                    //    newTriangles.Add(0);
                    //    newTriangles.Add(i + 2);
                    //    newTriangles.Add(i + 1);
                    //}
                    //return newTriangles.ToArray();



                    //Vector3 V1 = new Vector3(0, 0, 0);
                    //Vector3 V2 = new Vector3(0, 100, 0);
                    //Vector3 V3 = new Vector3(100, 100, 0);
                    //Vector3 V4 = new Vector3(100, 0, 0);

                    //Vector3[] newVertices;
                    //int[] newTriangles = new int[] { 0, 1, 2, 0, 2, 3 };


                    //newVertices = new Vector3[] { V1, V2, V3, V4 };

                    #endregion

                    break;

                #region not-squares
                case 20:
                    //triangle
                    break;

                case 21:
                    //corner triangle
                    break;

                case 30:
                    //diamond
                    break;

                case 40:

                    break;

                case 50:

                    break;

                case 60:

                    break;

                case 70:

                    break;
                    #endregion

            }




        }
    }

    void NewMesh(Vector3[] vertices, Vector2[] uv, int[] triangles) //because for some fucking reason, C# doesn't understand how Switch Statements work
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
    }


    private void Start()
    {
        //Fill();
    }

    // Update is called once per frame
    void Update()
    {
        if (changingShape)
        {
            Fill();
        }
        Mathf.Clamp(polygonSides, 1, 69);
    }

    #region Auto Shape stuff

    void DrawFilled(int sides, float radius)
    {
        polygonPoints = GetCircumferencePoints(sides, radius).ToArray();
        polygonTriangles = DrawFilledTriangles(polygonPoints);
        mesh.Clear();
        mesh.vertices = polygonPoints;
        mesh.triangles = polygonTriangles;
    }

    void DrawHollow(int sides, float radius, float innerRadius)
    {
        List<Vector3> pointsList = new List<Vector3>();
        List<Vector3> outerPoints = GetCircumferencePoints(sides, radius);
        pointsList.AddRange(outerPoints);
        List<Vector3> innerPoints = GetCircumferencePoints(sides, innerRadius);
        pointsList.AddRange(innerPoints);

        polygonPoints = pointsList.ToArray();

        polygonTriangles = DrawHollowTriangles(polygonPoints);
        mesh.Clear();
        mesh.vertices = polygonPoints;
        mesh.triangles = polygonTriangles;
    }

    int[] DrawHollowTriangles(Vector3[] points)
    {
        int sides = points.Length / 2;
        int triangleAmount = sides * 2;

        List<int> newTriangles = new List<int>();
        for(int i = 0; i < sides; i++)
        {
            int outerIndex = i;
            int innerIndex = i + sides;

            //first triangle starting at outer edge i
            newTriangles.Add(outerIndex);
            newTriangles.Add(innerIndex);
            newTriangles.Add((i+1)%sides);

            //second triangle starting at outer edge i
            newTriangles.Add(outerIndex);
            newTriangles.Add(sides+((sides+i-1)%sides));
            newTriangles.Add(outerIndex+sides);
        }
        return newTriangles.ToArray();
    }

    List<Vector3> GetCircumferencePoints (int sides, float radius)
    {
        List<Vector3> points = new List<Vector3>();
        float circumferenceProgressPerStep = (float) 1 / sides;
        float TAU = 2 * Mathf.PI;
        float radianProgressPerStep = circumferenceProgressPerStep * TAU;

        for(int i = 0; i<sides; i++)
        {
            float currentRadian = radianProgressPerStep * i;
            points.Add(new Vector3(Mathf.Cos(currentRadian) * radius, Mathf.Sin(currentRadian) * radius, 0));
        }
        return points;
    }

    int[] DrawFilledTriangles(Vector3[] points)
    {
        int triangleAmount = points.Length - 2;
        List<int> newTriangles = new List<int>();
        for(int i = 0; i<triangleAmount; i++)
        {
            newTriangles.Add(0);
            newTriangles.Add(i+2);
            newTriangles.Add(i+1);
        }
        return newTriangles.ToArray();
    }


    #endregion
}
