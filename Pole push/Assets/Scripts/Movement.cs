using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed;
    float ogSpeed;
    [HideInInspector]
    public Animator anim;
    [Range(0f,1f)]
    public float animationSpeed;
    public DragControll controls;
    public PoleBase poleBase;

    private Vector3 startAngle;
    private Vector3 targetAngle;
    private Vector3 turningStartPos;
    private Vector3 tempPos;
    private float turningTime;
    private float deltaDistance;
    private bool turning;
    private int direction;
    private int nextDirection;
    float time;
    [HideInInspector]
    public float distance;
    [HideInInspector]
    public static bool startGame;
    public GameObject tutorial;
    // Start is called before the first frame update
    void Start()
    {
        ogSpeed = speed;
        speed = 0f;
        anim = GetComponent<Animator>();
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
        distance += speed * Time.deltaTime;
    }

    void FixedUpdate()
    {
        if (startGame) { anim.SetFloat("Speed", animationSpeed); }
        if (Input.GetMouseButtonDown(0) && !startGame)
        {
            startGame = true;
            speed = ogSpeed;
            tutorial.SetActive(false);
        }
        if (poleBase.size <= 0)
        {
            anim.SetFloat("Aim Forward", 0);
            anim.SetFloat("Aim Left", 0);
            anim.SetFloat("Aim Right", 0);
            return;
        }
        if (!(controls.angle > 60.5) && !(controls.angle < -60.5) && startGame)
        {
            if (controls.angle > 0)
            {
                anim.SetFloat("Aim Left", 0);
                anim.SetFloat("Aim Right", (controls.angle / 10f) * 0.1f);
            }
            if (controls.angle < 0)
            {
                anim.SetFloat("Aim Right", 0);
                anim.SetFloat("Aim Left", (controls.angle / 10f) * -0.07f);
            }
        }
        else { anim.SetFloat("Aim Left", 0.4f); anim.SetFloat("Aim Right", 0.1f); }
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
