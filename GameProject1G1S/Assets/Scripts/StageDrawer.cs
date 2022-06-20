using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageDrawer : MonoBehaviour
{
    [SerializeField][Range(2, 100)] private int vertex;
    [SerializeField] private float radius;
    private LineRenderer lineRenderer;

    public int Vertex => vertex;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();

        Draw();
    }

    private void Draw()
    {
        lineRenderer.positionCount = vertex != 2 ? vertex + 1 : vertex;
        float tau = 2 * Mathf.PI;

        for (int i = 0; vertex != 2 ? i <= vertex : i < vertex; i++)
        {
            float angle = ((float)i / vertex) * tau;
            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;
            lineRenderer.SetPosition(i, new Vector3(x, y, 0));
        }
    }
}
