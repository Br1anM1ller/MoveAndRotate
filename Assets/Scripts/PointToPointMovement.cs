using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointToPointMovement : MonoBehaviour
{
    public Vector3[] points;
    private int currentPointIndex = 0;
    private bool movingForward = true;
    private bool isMoving = false;
    public float speed = 5f;

    void Update()
    {
        if (isMoving)
        {
            if (currentPointIndex >= points.Length || currentPointIndex < 0)
            {
                movingForward = !movingForward;
                currentPointIndex += movingForward ? 1 : -1;
            }

            Vector3 direction = (points[currentPointIndex] - transform.position).normalized;

            if (direction != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(direction);
            }

            transform.position = Vector3.MoveTowards(transform.position, points[currentPointIndex], speed * Time.deltaTime);

            if (transform.position == points[currentPointIndex])
            {
                currentPointIndex += movingForward ? 1 : -1;
            }
        }
    }

    public void ToggleMovement()
    {
        isMoving = !isMoving;
    }
}
