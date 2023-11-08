using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryLine : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private int lineSegments = 60;
    [SerializeField] private float timeOfTheFlight = 5;

    public void ShowTrajectoryLine(Vector3 startPoint, Vector3 startVelocity)
    {
        float timeStep = timeOfTheFlight / lineSegments;
        Vector3[] lineRendererPoints = CalculateTrajectoryLine(startPoint, startVelocity, timeStep);
        _lineRenderer.positionCount = lineSegments;
        _lineRenderer.SetPositions(lineRendererPoints);
    }

    private Vector3[] CalculateTrajectoryLine(Vector3 startPoint, Vector3 startVelocity, float timeStep)
    {
        Vector3[] lineRendererPoints = new Vector3[lineSegments];
        lineRendererPoints[0] = startPoint;
        for (int i = 1; i < lineSegments; i++)
        {
            float timeOffset = timeStep * i;
            Vector3 progressBeforeGravity = startVelocity * timeOffset;
            Vector3 gravityOffset = Vector3.up * -0.5f * Physics.gravity.y * timeOffset * timeOffset;
            Vector3 newPos = startPoint + progressBeforeGravity - gravityOffset;
            lineRendererPoints[i] = newPos;
        }

        return lineRendererPoints;
    }

    public void SetLineVisible(bool show)
    {
        if (show) _lineRenderer.gameObject.SetActive(true);
        else _lineRenderer.gameObject.SetActive(false);
    }
}