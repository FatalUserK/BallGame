using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using UnityEngine.UIElements;


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
        Debug.Log(shape);
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
            List<Vector3> points = new List<Vector3>();
            switch (shape)
            {
                case 0:
                    //impossible lmao
                    break;

                case 1:
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
                        1, 0, 2,
                        3, 1, 2
                    };

                    NewMesh(vertices, uv, triangles, 0, false);



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
                case 2:
                    //corner triangle

                    vertices = new Vector3[]
                    {
                        new Vector3(0, 0),
                        new Vector3(1, 0),
                        new Vector3(0, 1)
                    };

                    uv = new Vector2[]
                    {
                        new Vector2(0, 0),
                        new Vector2(1, 0),
                        new Vector2(0, 1)
                    };

                    triangles = new int[]
                    {
                        1, 0, 2
                    };


                    Debug.Log("MOVING ONTO CREATE NEWMESH");
                    NewMesh(vertices, uv, triangles, rotationSetting: 1, false);

                    break;

                case 3:
                    //triangle



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

    void NewMesh(Vector3[] vertices, Vector2[] uv, int[] triangles, int rotationSetting = 0, bool allignedCorrectly = true) //because for some fucking reason, C# doesn't understand how Switch Statements work
    {
        Debug.Log("NEWMESH STARTED, ROTATION SETTING IS " + rotationSetting);
        if (!allignedCorrectly)
        {
            for (int j = 0; j < vertices.Length; j++)
            {
                vertices[j].x = vertices[j].x - (float).5;
                vertices[j].y = vertices[j].y - (float).5;
                uv[j].x = uv[j].x - (float).5;
                uv[j].y = uv[j].y - (float).5;
            }
        }

        switch (rotationSetting) // 0: none. 1: cardinal. 2: cardinal & diagonal
        {
            default:
                //do nothing lmao
                break;

            case 1:
                transform.localRotation = Quaternion.Euler(0, 0, Random.Range(0, 4) * 90);
                break;

            case 2:
                float magicNumber = new System.Random().Next(0, 7) * 45;
                Vector3 john = new Vector3(0, 0, 0); // note: remove johns

                transform.Rotate(0, 0, magicNumber);
                if (magicNumber %2 != 0) { transform.localScale = new Vector3(.6f, .6f, .6f); }
                break;


        }
            


        Vector2[] newVerts = new Vector2[vertices.Length];
        int i = 0;
        foreach (Vector3 v3 in vertices)
        {
            newVerts[i] = new Vector2(v3.x, v3.y);
            i++;
        }

        mesh.Clear();
        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

        



        //Vector2[] myPoints = new Vector2[vertices.Length - 1];
        //for (i = 0; i < vertices.Length - 1; i++)
        //{
        //    myPoints[i] = vertices[i];
        //}

        //gameObject.AddComponent<PolygonCollider2D>();
        //GetComponent<PolygonCollider2D>().points = myPoints;

        // Omiixi's code lmao ^



        autoPolygonBS(vertices, triangles);



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



    void autoPolygonBS(Vector3[] vertices, int[] triangles)
    {
        // Get just the outer edges from the mesh's triangles (ignore or remove any shared edges)
        Dictionary<string, KeyValuePair<int, int>> edges = new Dictionary<string, KeyValuePair<int, int>>();
        for (int i = 0; i < triangles.Length; i += 3)
        {
            for (int e = 0; e < 3; e++)
            {
                int vert1 = triangles[i + e];
                int vert2 = triangles[i + e + 1 > i + 2 ? i : i + e + 1];
                string edge = Mathf.Min(vert1, vert2) + ":" + Mathf.Max(vert1, vert2);
                if (edges.ContainsKey(edge))
                {
                    edges.Remove(edge);
                }
                else
                {
                    edges.Add(edge, new KeyValuePair<int, int>(vert1, vert2));
                }
            }
        }

        // Create edge lookup (Key is first vertex, Value is second vertex, of each edge)
        Dictionary<int, int> lookup = new Dictionary<int, int>();
        foreach (KeyValuePair<int, int> edge in edges.Values)
        {
            if (lookup.ContainsKey(edge.Key) == false)
            {
                lookup.Add(edge.Key, edge.Value);
            }
        }

        // Create empty polygon collider
        PolygonCollider2D polygonCollider = gameObject.GetComponent<PolygonCollider2D>();
        polygonCollider.pathCount = 0;

        // Loop through edge vertices in order
        int startVert = 0;
        int nextVert = startVert;
        int highestVert = startVert;
        List<Vector2> colliderPath = new List<Vector2>();
        while (true)
        {

            // Add vertex to collider path
            colliderPath.Add(vertices[nextVert]);

            // Get next vertex
            nextVert = lookup[nextVert];

            // Store highest vertex (to know what shape to move to next)
            if (nextVert > highestVert)
            {
                highestVert = nextVert;
            }

            // Shape complete
            if (nextVert == startVert)
            {

                // Add path to polygon collider
                polygonCollider.pathCount++;
                polygonCollider.SetPath(polygonCollider.pathCount - 1, colliderPath.ToArray());
                colliderPath.Clear();

                // Go to next shape if one exists
                if (lookup.ContainsKey(highestVert + 1))
                {

                    // Set starting and next vertices
                    startVert = highestVert + 1;
                    nextVert = startVert;

                    // Continue to next loop
                    continue;
                }

                // No more verts
                break;
            }
        }
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
