using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [SerializeField] private Ball ballPrefab;
    [SerializeField] private BallPosVisualizer ballPositioner;
    [SerializeField] private Transform hoopTransform;
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
        
        var lookRotation = Quaternion.LookRotation(hoopTransform.position - ball.transform.position);
        ball.transform.rotation = lookRotation;
        
        _cam.transform.position = ball.camPos.position;
        _cam.transform.rotation = ball.camPos.rotation;
    }
}