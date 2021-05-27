using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarMovement : MonoBehaviour
{
    public float speed;
    private Vector3 startAngle;
    private Vector3 targetAngle;
    private Vector3 turningStartPos;
    private Vector3 tempPos;
    private float turningTime;
    private float deltaDistance;
    private bool turning;
    private int direction;
    private int nextDirection;
    public ProgressBar pBar;
    float time;
    float distance;

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("BarFinish"))
        {
            speed = 0f;
            pBar.levelDistance = distance;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (turning)
        {
            if (direction == 0)
            {
                deltaDistance = transform.position.z - turningStartPos.z;
            }
            if (direction == 1)
            {
                deltaDistance = transform.position.x - turningStartPos.x;
            }
            if (direction == 2)
            {
                deltaDistance = -(transform.position.z - turningStartPos.z);
            }
            if (direction == 3)
            {
                deltaDistance = -(transform.position.x - turningStartPos.x);
            }
            time += Time.deltaTime;
            transform.localEulerAngles = Vector3.Lerp(startAngle, targetAngle, time/turningTime);
            if (time / turningTime >= 1)
            {
                transform.localEulerAngles = targetAngle;
                direction = nextDirection;
                turning = false;
            }
        }
        transform.position += (transform.rotation * Vector3.forward *speed) * Time.deltaTime;
        distance += speed*Time.deltaTime;
    }

    public void TurnCorner(bool left)
    {
        time = 0;
        startAngle = transform.localEulerAngles;
        targetAngle = startAngle;
        if (left)
        {
            nextDirection -= 1;
            targetAngle.y -= 90;
        }
        else
        {
            nextDirection += 1;
            targetAngle.y += 90;
        }

        if (nextDirection == -1)
        {
            nextDirection = 3;
        }
        if (nextDirection == 4)
        {
            nextDirection = 0;
        }

        turningStartPos = transform.position;
        turningTime = 3.4f*10f/speed;
        turning = true;
    }
}
