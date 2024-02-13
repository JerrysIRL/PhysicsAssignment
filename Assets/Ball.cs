using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(DrawTrajectory))]
public class Ball : MonoBehaviour
{
    [SerializeField] private float forceMulti = 3f;
    private Rigidbody _rb;
    private Vector3 _mousePressDownPos;
    private Vector3 _mousePressUpPos;
    private bool isShooting;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void OnMouseDown()
    {
        Debug.Log("hovering");
        _mousePressDownPos = Input.mousePosition;
    }

    private void OnMouseUp()
    {
        Debug.Log("Released");
        Shoot(_mousePressDownPos - _mousePressUpPos);
    }

    private void OnMouseDrag()
    {
        _mousePressUpPos = Input.mousePosition;
        Debug.Log("Holding");
    }

    void Shoot(Vector3 force)
    {
        /*if (isShooting)
        {
            return;
        }*/

        //new Vector3(force.x, force.y, force.y)
        Vector3 myforce = Camera.main.transform.TransformDirection(force) * forceMulti;
        _rb.AddForce(myforce);
        isShooting = true;
    }
}