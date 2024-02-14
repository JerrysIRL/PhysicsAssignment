using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class TrajectoryVisualizer : MonoBehaviour
{
    [SerializeField] [Range(3, 30)] private int lineSegments = 20;
    private readonly List<Vector3> _pointsList = new List<Vector3>();

    private LineRenderer _lineRenderer;

    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }
    
    public void UpdateTrajectory(Vector3 forceVector, Rigidbody rb, Vector3 startingPoint)
    {
        Vector3 velocity = (forceVector / rb.mass) * Time.fixedDeltaTime;
        float airDuration = (2 * velocity.y) / Physics.gravity.y; 
        float stepTime = airDuration / lineSegments;

        _pointsList.Clear();
        for (int i = 0; i < lineSegments; i++)
        {
            float stepTimePassed = stepTime * i;

            Vector3 movementVector = new Vector3(
                velocity.x * stepTimePassed,
                velocity.y * stepTimePassed - 0.5f * Physics.gravity.y * stepTimePassed * stepTimePassed,
                velocity.z * stepTimePassed
            );
            
            _pointsList.Add(-movementVector + startingPoint);
        }
        
        _lineRenderer.positionCount = lineSegments;
        _lineRenderer.SetPositions(_pointsList.ToArray());
    }

    public void ClearTrajectory()
    {
        _lineRenderer.positionCount = 0;
    }
}