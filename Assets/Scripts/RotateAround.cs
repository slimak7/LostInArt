using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour {

    public int minRot;
    public int maxRot;

    public Vector3 direction;

    public Transform point;

    public int speed;

    public void rotate(float angle)
    {
        StartCoroutine(startRotating(angle));
    }

    private IEnumerator startRotating(float angle)
    {
        float rotationAngle = angle;

        float s = speed * Time.deltaTime;

        while (rotationAngle > 0)
        {
            transform.RotateAround(point.position, point.transform.forward, s);

            rotationAngle -= s;

            yield return null;

        }
    }
}
