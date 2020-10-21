using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderForShooting : MonoBehaviour
{
    [Header("Debug Field")]
    public float moveSpeed = 3f;
    public float rotationSpeed = 50f;
    public float maxDistance;

    private bool isWandering = false;
    private bool isRotatingRight = false;
    private bool isRotatingLeft = false;
    private bool isMovingForward = false;
    //private bool isFlying = false;
    private bool isUp = false;
    private bool isDown = false;


    private float rotationTime;
    private float rotationWaitTime;
    private float rotateLeftOrRight;
    private float moveThanWait;
    private float timeTomove;
    private bool m_isUp;

    private Transform m_target = null;
    private Vector3 m_rotation;
    private float m_distance;
    private float[] randomSeed;
    private Coroutine wanderCoroutine = null;
    private Coroutine upCoroutine = null;
    private Coroutine downCoroutine = null;
    [Header("Unity Configuration Field")]
    public Rigidbody rb;
    public LayerMask layerMask;
    public LayerMask layerMask1;
    RaycastHit hit;


    AvoidObstacle avoidObstacle;
    RotateObject m_rotateObject;

    Transform targetTransform;

    // Start is called before the first frame update
    void Start()
    {
        Random.InitState(42);
        randomSeed = new float[10];
        for (int i = 0; i < randomSeed.Length; i++)
        {
            randomSeed[i] = Random.Range(0, 5);
            Debug.Log(randomSeed[i]);
        }

        m_rotateObject = GetComponent<RotateObject>();
        avoidObstacle = GetComponent<AvoidObstacle>();
    }

    // Update is called once per frame
    public void DoWander()
    {
        if (isWandering == false)
        {
            //StartCoroutine("Wander");
            if (wanderCoroutine == null)
            {
                wanderCoroutine = StartCoroutine("Wander");
            }
            else
            {
                StopCoroutine(wanderCoroutine);
                //Debug.Log(wanderCoroutine);
                wanderCoroutine = null;
            }
        }

        if (isRotatingRight == true)
        {
            rb.angularVelocity = new Vector3(0, rotationSpeed, 0);
        }
        else
        {
            rb.angularVelocity = Vector3.zero;
        }


        if (isRotatingLeft == true)
        {
            rb.angularVelocity = new Vector3(0, -rotationSpeed, 0);
        }
        else
        {
            rb.angularVelocity = Vector3.zero;
        }



        if (isMovingForward == true)
        {
            rb.AddForce(transform.forward * moveSpeed);

        }
        else
        {
            rb.velocity = Vector3.zero;
        }

    }


    void Update()
    {
        if (Physics.BoxCast(center: transform.position, halfExtents: transform.lossyScale / 2, direction: transform.forward,
           out hit, transform.rotation, maxDistance, layerMask1))
        {
            rb.angularVelocity = new Vector3(0, rotationSpeed - 90, 0);
        }

        if (transform.rotation.x != 0 && m_target == null)
        {
            transform.rotation = Quaternion.Euler(0f, m_rotation.y, 0f);
        }
        if (m_target != null)
        {
            isMovingForward = false;
        }

    }

        


    IEnumerator Wander()
    {

        float randomLeftorRight = Random.Range(0, 3);
        float randomUporDown = Random.Range(0, 3);
        rotationTime = randomSeed[0];
        rotationWaitTime = randomLeftorRight;
        rotateLeftOrRight = randomUporDown;

        float upTime = randomSeed[3];
        float uporDownWaitTime = randomLeftorRight;
        float goUpOrDown = randomLeftorRight;

        moveThanWait = randomSeed[0];
        timeTomove = randomSeed[0];


        isWandering = true;

        //Debug.Log(randomUporDown);

        yield return new WaitForSeconds(moveThanWait);
        isMovingForward = true;
        yield return new WaitForSeconds(timeTomove);
        isMovingForward = false;

        yield return new WaitForSeconds(rotationWaitTime);
        if (rotateLeftOrRight == 1)
        {
            isRotatingRight = true;
            yield return new WaitForSeconds(rotationTime);
            isRotatingRight = false;
        }

        if (rotateLeftOrRight == 2)
        {
            isRotatingLeft = true;
            yield return new WaitForSeconds(rotationTime);
            isRotatingLeft = false;
        }


        yield return new WaitForSeconds(uporDownWaitTime);
        if (goUpOrDown == 1)
        {
            Debug.Log("prepare fly");
            isUp = true;
            yield return new WaitForSeconds(upTime);
            isUp = false;
        }

        yield return new WaitForSeconds(uporDownWaitTime);
        if (goUpOrDown == 2)
        {
            Debug.Log("prepare go down");
            isDown = true;
            yield return new WaitForSeconds(upTime);
            isDown = false;
        }


        isWandering = false;
    }

    private void OnDrawGizmos()
    {
        if (Physics.BoxCast(center: transform.position, halfExtents: transform.lossyScale / 2, direction: transform.forward,
            out hit, transform.rotation, maxDistance))
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(center: transform.position + transform.forward * hit.distance, size: transform.lossyScale);
        }
        else
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(from: transform.position, direction: (transform.forward) * maxDistance);
        }
    }


    public void SetRotation(Vector3 rotate)
    {
        m_rotation = rotate;
    }


    public void SetTarget(Transform target)
    {
        m_target = target;
    }

}
