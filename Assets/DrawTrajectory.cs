using System.Collections.Generic;
using UnityEngine;

public class DrawTrajectory : MonoBehaviour
{
    [SerializeField] [Range(3, 30)] private int lineSegments = 20;

    private LineRenderer _lineRenderer;
    private List<Vector3> pointsList = new List<Vector3>();

    // Start is called before the first frame update
    public void UpdateTrajectory(Vector3 forceVector, Rigidbody rb, Vector3 startinPoint)
    {
        Vector3 velocity = (forceVector / rb.mass) * Time.fixedDeltaTime;
        float airDuration = (2 * velocity.y) / Physics.gravity.y;
        float stepTime = airDuration / lineSegments;

        pointsList.Clear();
        for (int i = 0; i < lineSegments; i++)
        {
            float stepTimePassed = stepTime * i;

            Vector3 movementVector = new Vector3(
                velocity.x * stepTimePassed,
                velocity.y * stepTimePassed,
                velocity.z * stepTimePassed
            );

            pointsList.Add(-movementVector + startinPoint);
        }

        _lineRenderer.positionCount = lineSegments;
        _lineRenderer.SetPositions(pointsList.ToArray());
    }
}