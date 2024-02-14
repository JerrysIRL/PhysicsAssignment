using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(TrajectoryVisualizer))]
public class Ball : MonoBehaviour
{
    [SerializeField] private float forceMulti = 3f;
    [SerializeField] private float verticalForceMulti = 3f;
    [SerializeField] public Transform camPos;

    public Action ShootAction;
    private TrajectoryVisualizer _trajectoryVisualizer;
    private Rigidbody _rb;
    private Vector3 _mousePressDownPos;
    private Vector3 _mousePressUpPos;
    private bool _isShooting;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _trajectoryVisualizer = GetComponent<TrajectoryVisualizer>();
    }

    private void OnMouseDown()
    {
        _mousePressDownPos = Input.mousePosition;
    }

    private void OnMouseUp()
    {
        _mousePressUpPos = Input.mousePosition;
        Shoot(_mousePressDownPos - _mousePressUpPos);
        _trajectoryVisualizer.ClearTrajectory();
        ShootAction?.Invoke();
        Destroy(gameObject, 5f);
    }

    private void OnMouseDrag()
    {
        if (_isShooting)
            return;

        Vector3 direction = Input.mousePosition - _mousePressDownPos;
        Vector3 force = new Vector3(direction.x, direction.y * verticalForceMulti, direction.y) * forceMulti;
        _trajectoryVisualizer.UpdateTrajectory(transform.TransformDirection(force), _rb, transform.position);
    }

    void Shoot(Vector3 direction)
    {
        if (_isShooting)
        {
            return;
        }

        Vector3 myforce = new Vector3(direction.x, direction.y * verticalForceMulti, direction.y) * forceMulti;
        _rb.AddForce(transform.TransformDirection(myforce));
        _isShooting = true;
    }
}