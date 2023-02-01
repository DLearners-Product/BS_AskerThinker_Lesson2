using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class MakePolygonCollider2D
{
    // Supply the verts in go-around-the-edge order. Unity winds clockwise.
    // It's up to you to supply at least three points.
    // No idea what Physics2D might do with whacky interleaved colliders... try it!!
    public static GameObject Create(Vector2[] SourcePoints, bool DoubleSided = false)
    {
        Vector2[] Points = new Vector2[SourcePoints.Length];
        System.Array.Copy(SourcePoints, Points, SourcePoints.Length);

        GameObject go = new GameObject("MakeCollider2D");

        Mesh mesh = new Mesh();

        Vector2 centroid = Vector3.zero;

        for (int i = 0; i < Points.Length; i++)
        {
            centroid += Points[i];
        }

        centroid /= Points.Length;

        for (int i = 0; i < Points.Length; i++)
        {
            Points[i] -= centroid;
        }

        go.transform.position = centroid;

        int numSides = DoubleSided ? 2 : 1;

        using (var vh = new VertexHelper())
        {
            int vertCounter = 0;

            for (int sideNo = 0; sideNo < numSides; sideNo++)
            {
                for (int i = 0; i < Points.Length; i++)
                {
                    // second side just considers the points in reverse
                    int iMapped = (sideNo == 0) ? i : ((Points.Length - 1) - i);

                    Vector2 vert = Points[iMapped];

                    UIVertex vtx = new UIVertex();

                    vtx.position = new Vector3(vert.x, vert.y, 0);

                    vtx.uv0 = new Vector2(vert.x, vert.y);

                    vh.AddVert(vtx);

                    if (((i > 1) && (i < Points.Length)) ||
                        (i > Points.Length + 1))
                    {
                        // topology is a fan
                        if (sideNo == 0)
                        {
                            vh.AddTriangle(0, vertCounter - 1, vertCounter);
                        }
                        if (sideNo == 1)
                        {
                            vh.AddTriangle(Points.Length, vertCounter - 1, vertCounter);
                        }
                    }

                    vertCounter++;
                }
            }

            vh.FillMesh(mesh);
        }

        //creating polygon collider 2d and adding it to a gameobject
        var pc2d = go.AddComponent<PolygonCollider2D>();
        Debug.Log(Points);
        pc2d.points = Points;

        mesh.RecalculateBounds();
        mesh.RecalculateNormals();

        MeshFilter mf = go.AddComponent<MeshFilter>();
        mf.mesh = mesh;

        go.AddComponent<MeshRenderer>();

        return go;
    }
}