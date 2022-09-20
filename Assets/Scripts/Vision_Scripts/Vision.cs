using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class Vision : MonoBehaviour
{
    [SerializeField] private float fov = 360f;
    [SerializeField] private int rayCount = 50;
    [SerializeField] private float viewDistance = 50f;
    [SerializeField] private LayerMask ignoreObject;

    private Mesh mesh;
    private float angle;
    private float angleIncrease;
    private Vector3[] vertices;
    private Vector2[] uv;
    private int[] triangles;
    private Vector3 origin;
    private float startingAngle;
    
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        angleIncrease = fov / rayCount;
        vertices = new Vector3[rayCount + 2];
        uv = new Vector2[vertices.Length];
        triangles = new int[rayCount * 3];
    }

    void Update()
    {
        int vertexIndex = 1;
        int triangleIndex = 0;
        
        angle = startingAngle;
        origin = transform.position;
        vertices[0] = origin;
        
        for (int i = 0; i <= rayCount; i++)
        {
            Vector3 vertex;
            var raycast = Physics2D.Raycast(origin, UtilsClass.GetVectorFromAngle(angle), viewDistance,ignoreObject);
            
            Debug.DrawRay(origin, UtilsClass.GetVectorFromAngle(angle) * viewDistance, Color.green);
            if (raycast.collider is null)
            {
                vertex = origin + UtilsClass.GetVectorFromAngle(angle) * viewDistance;
            }
            else
            {
                vertex = raycast.point;
            }
            
            vertices[vertexIndex] = vertex;

            if (i > 0)
            {
                triangles[triangleIndex + 0] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;

                triangleIndex += 3;
            }

            vertexIndex++;
            angle -= angleIncrease;
        }

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
    }

    private void SetOrigin(Vector3 origin)
    {
        this.origin = origin;
    }

    private void SetAimDirection(Vector3 aimDirection)
    {
        startingAngle = UtilsClass.GetAngleFromVectorFloat(aimDirection) - fov/2f;
    }
}
