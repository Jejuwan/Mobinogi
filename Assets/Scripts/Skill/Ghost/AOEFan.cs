using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.HableCurve;

public class AOEFan : AOESkill
{
    float radius = 2f;
    float angleDegree = 40f;
    int segments = 5;
    [SerializeField] float xAngle = 0f;
    [SerializeField] float yAngle = 0f;
    [SerializeField] float zAngle = 0f;

    protected override void Awake()
    {
        base.Awake();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        CreateCollider();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    private void CreateCollider()
    {
        Mesh mesh = new Mesh();

        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();

        vertices.Add(Vector3.zero); // center

        float angleRad = Mathf.Deg2Rad * angleDegree;
        Quaternion rotation = Quaternion.Euler(xAngle, yAngle, zAngle); // 원하는 축 회전값 입력
        for (int i = 0; i <= segments; i++)
        {
            float t = i / (float)segments;
            float currentAngle = t * angleRad;
            float x = radius * Mathf.Sin(currentAngle);
            float z = radius * Mathf.Cos(currentAngle);

            Vector3 point = new Vector3(x, 0, z);
            vertices.Add(rotation * point); // 축 회전 적용
        }

        for (int i = 1; i < vertices.Count - 1; i++)
        {
            triangles.Add(0);
            triangles.Add(i);
            triangles.Add(i + 1);
        }

        mesh.SetVertices(vertices);
        mesh.SetTriangles(triangles, 0);
        mesh.RecalculateNormals();

        GetComponent<MeshFilter>().mesh = mesh;
        GetComponent<MeshCollider>().sharedMesh = mesh;
    }
}
