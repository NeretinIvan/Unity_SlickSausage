using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class ChargeLineDrawer : MonoBehaviour
{
    [HideInInspector()] public Vector2 startingPoint;
    [HideInInspector()] public Vector2 endingPoint;
    private LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        startingPoint = Vector2.zero;
        endingPoint = Vector2.zero;
        Deactivate();
    }

    public void SetLinePositions(Vector2 startingPoint, Vector2 endingPoint)
    {
        this.startingPoint = startingPoint;
        this.endingPoint = endingPoint;
    }

    public void Activate()
    {
        lineRenderer.positionCount = 2;
    }

    public void Deactivate()
    {
        lineRenderer.positionCount = 0;
    }

    private void Update()
    {
        /*
        if (lineRenderer.positionCount >= 2)
        {
            lineRenderer.SetPosition(0, startingPoint);
            lineRenderer.SetPosition(1, endingPoint);
        }
        */
    }
}
