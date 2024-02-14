
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [SerializeField] private Ball ballPrefab;
    [SerializeField] private BallPosVisualizer ballPositioner;
    [SerializeField] private Transform hoopTransform;
    [SerializeField] private Vector3 cameraOffset;
    private Camera _cam;

    private void Start()
    {
        _cam = Camera.main;
        SpawnNewBall();
    }
    

    private void SpawnNewBall()
    {
        Vector3 pos = ballPositioner.GetRandomPointOnBezier();
        Ball ball = Instantiate(ballPrefab, pos, Quaternion.identity);
        ball.ShootAction += SpawnNewBall;
        AdjustCamera(ball.transform.position);
    }

    private void AdjustCamera(Vector3 ballpos)
    {
        var direction = hoopTransform.position - ballpos;
        
        _cam.gameObject.transform.position = ballpos + cameraOffset;
        
    }
}
