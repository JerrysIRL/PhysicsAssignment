using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPosVisualizer : MonoBehaviour
{
    [SerializeField] private Transform left, mid, right;
    [SerializeField] private Transform leftControl, rightControl;
    public int curvePoints = 10;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(left.position, 2f);
        Gizmos.DrawWireSphere(mid.position, 2f);
        Gizmos.DrawWireSphere(right.position, 2f);

        Gizmos.color = Color.blue;
        Vector3 pointInBetween = Bezier(left.position, leftControl.position, mid.position, 0.5f);
        Gizmos.DrawWireSphere(pointInBetween, 2f);
        Vector3 startPos;
        Vector3 endPos;
        for (int i = 0; i < curvePoints; i++)
        {
            startPos = Bezier(left.position, leftControl.position, mid.position, (1f / curvePoints) * i);
            endPos = Bezier(left.position, leftControl.position, mid.position, (1f / curvePoints) * (i + 1));
            Gizmos.DrawLine(startPos, endPos);
        }

        for (int i = 0; i < curvePoints; i++)
        {
            startPos = Bezier(right.position, rightControl.position, mid.position, (1f / curvePoints) * i);
            endPos = Bezier(right.position, rightControl.position, mid.position, (1f / curvePoints) * (i + 1));
            Gizmos.DrawLine(startPos, endPos);
        }
    }

    private bool flip;

    public Vector3 GetRandomPointOnBezier()
    {
        flip = !flip;
        float randomFloat = Random.Range(0f, 1f);

        return flip ? Bezier(right.position, rightControl.position, mid.position, randomFloat) : Bezier(left.position, leftControl.position, mid.position, randomFloat);
    }

    [ContextMenu("RandomPoint")]
    void PrintRandomPoint()
    {
        Debug.Log(GetRandomPointOnBezier());
    }


    Vector3 Bezier(Vector3 a, Vector3 b, float t)
    {
        return Vector3.Lerp(a, b, t);
    }

    Vector3 Bezier(Vector3 a, Vector3 b, Vector3 c, float t)
    {
        return Vector3.Lerp(Bezier(a, b, t), Bezier(b, c, t), t);
    }
}