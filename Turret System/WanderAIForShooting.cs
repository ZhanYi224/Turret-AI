using System.Collections;
using UnityEngine;

public class WanderAIForShooting : MonoBehaviour
{


    [Header("Debug Field")]
    public float moveSpeed = 3f;
    public float rotationSpeed = 50f;
    public float maxDistance;

    public float raycastMaxDistance;

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
    //private bool onDetect = true;


    [Header("Unity Configuration Field")]
    public Rigidbody rb;
    public LayerMask layerMask;
    public LayerMask layerMask1;
    RaycastHit hit;
    RaycastHit secondHit;

    public GameObject wallPosition;
    //public GameObject border;

    private bool to_rotate;
   
 
    float distnace_to_border = Mathf.Infinity;

    AvoidObstacle avoidObstacle;
    //RotateObject m_rotateObject;
    SensorAI m_moveTowardTarget;
    RotateObject rttObject;


    //private Vector3 frontPos = new Vector3(0f,0.2f,0.5f);
    //private float frontSenorAngle = 30f;
    //private float m_aiDistance;

    Transform targetTransform;

    Transform moveTargetPosition;
    Vector3 end_of_position;


    // Vector3 aiDistance;

    //AvoidObstacle avoidObstacle;

    // Start is called before the first frame update
    void Start()
    {
        Random.InitState(42);
        randomSeed = new float[10];
        for (int i = 0; i < randomSeed.Length; i++)
        {
            randomSeed[i] = Random.Range(0, 5);
            //Debug.Log(randomSeed[i]);
        }

        //m_rotateObject = GetComponent<RotateObject>();
        m_moveTowardTarget = GetComponent<SensorAI>();
        avoidObstacle = wallPosition.GetComponent<AvoidObstacle>();
        rttObject = GetComponent<RotateObject>();

    }




    // Update is called once per frame
    void Update()
    {
        #region here is something dont need
        //if (isFlying == true)
        //{
        //    Debug.Log("start to fly");
        //    rb.velocity = new Vector3(0, 1f, 0);
        //}


        //if (transform.position.y <= 2)
        //{
        //    isFlying = true;
        //}
        //else
        //{
        //    isFlying = false;
        //}

        #endregion

        #region Code that let turret move up and down

        if (m_isUp == true)
        {
            rb.AddForce(transform.up * 2f);
        }
        else
        {
            rb.AddForce(-transform.up * 2f);
        }

        if (isUp == true)
        {
            Debug.Log("Start flying");
            //rb.velocity = new Vector3(0, 1f, 0);
            rb.AddForce(transform.up * 2f);
        }


        if (isDown == true)
        {
            Debug.Log("Go down");
            //rb.velocity = new Vector3(0, -10f, 0);
            rb.AddForce(-transform.up * 2f);
        }

        if (transform.position.y > 6)
        {
           
            if (upCoroutine == null)
            {
                upCoroutine = StartCoroutine("BeginUp");
            }
            else
            {
                StopCoroutine(upCoroutine);
                upCoroutine = null;
            }

        }

        if (transform.position.y < 0)
        {
            if (downCoroutine == null)
            {
                downCoroutine = StartCoroutine("BeginDown");
            }
            else
            {
                StopCoroutine(downCoroutine);
                downCoroutine = null;
            }

        }

        if (transform.position.y > 7)
        {
            transform.rotation = Quaternion.Euler(0f, m_rotation.y, 0f);
        }

        //if(transform.position.y < 2 && transform.rotation.x > 3)
        //{
        //    transform.rotation = Quaternion.Euler(0, m_rotation.y, 0f);
        //}


        #endregion

        #region Code that make the AI wander

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
            Debug.Log("walking");
            //rb.AddForce(transform.forward * moveSpeed); //use addforce it wont move up when it rotation is looking up already

            
            rb.velocity = transform.forward * moveSpeed;
        }
        else
        {
            Debug.Log("Not walking");
            rb.velocity = Vector3.zero;

        }
        #endregion


        //if (m_target != null) // this line of code need to be in a class and when shooting call this code in wanderAiForShooting. 16/8/2020 remember to fix it
        //{
        //    isMovingForward = false;
        //}






        /*
         * if boxcast hit border   (done)
         *      if boxcast hit wall (done)
         *          call avoid obstacle code to rotate and look for nearest place to rotate than set it to rotate(done)
         *          save vector3 end point of boxcast on wall into a temporary data (done)
         *      if AI pass true the wall call true (done)
         *          set the vector3 end point direction for AI to rotate
         * else if boxcast hit wall
         *      call rotate and look for nearest place to rotate
         *      
         * if distance is less than vector3 of end point boxcast
         *      set vector3 of end_of_position back to null
         *     
         */
        if (Physics.BoxCast(center: transform.position, halfExtents: transform.lossyScale / 2, direction: transform.forward,
             out secondHit, transform.rotation, raycastMaxDistance, layerMask1))
        {
            //save the secondhit.point into a temporary data here
            if (Physics.BoxCast(center: transform.position, halfExtents: transform.lossyScale / 2, direction: transform.forward,
                 out hit, transform.rotation, maxDistance / 2, layerMask))
            {
                targetTransform = avoidObstacle.DetectTurret(transform.position);
                m_moveTowardTarget.SetTarget(targetTransform);

                Debug.Log(end_of_position);
                end_of_position = secondHit.point;

            }
            if (to_rotate == true)
            {
                rttObject.SetTempDirection(end_of_position);
                to_rotate = false;
            }
            if (transform.position == end_of_position)
            {
                Debug.Log("set vecotr3 end of position to null");
            }
        }

        else if (Physics.BoxCast(center: transform.position, halfExtents: transform.lossyScale / 2, direction: transform.forward,
                 out hit, transform.rotation, maxDistance / 2, layerMask))
        {
            targetTransform = avoidObstacle.DetectTurret(transform.position);
            m_moveTowardTarget.SetTarget(targetTransform);
        }

        //if (Physics.BoxCast(center: transform.position, halfExtents: transform.lossyScale / 2, direction: transform.forward,
        //    out secondHit, transform.rotation, raycastMaxDistance/4, layerMask1))
        //{
        //    rb.angularVelocity = new Vector3(0, rotationSpeed - 90, 0);
        //}



        #region here is to make the turret turn 90 degre when it detect wall (close already)
        //here is to let the turret avoid wall
        /*
        if (isRaycasting == true)
        {
            if (Physics.BoxCast(center: transform.position, halfExtents: transform.lossyScale / 2, direction: transform.forward,
            out hit, transform.rotation, raycastMaxDistance, layerMask1))
            {
                Debug.Log("is ON");
                end_of_position = hit.point;
                Debug.Log(end_of_position);
                rttObject.SetTempDirection(end_of_position);
                //rb.angularVelocity = new Vector3(0, rotationSpeed - 90, 0);
            }
        }

        */



        #endregion

        #region
        /*

        //here is use raycast physics to tell the turret turn


        if (Physics.BoxCast(center: transform.position, halfExtents: transform.lossyScale / 2, direction: transform.forward,
        out hit, transform.rotation, maxDistance, layerMask))
        {
            targetTransform = avoidObstacle.DetectTurret(transform.position);

           // Debug.Log(" after call avoid obstacle set to" + targetTransform);
          //m_rotateObject.SetTarget(targetTransform);//change it, pass the value back to the sensorAI -> m_target there
            m_moveTowardTarget.SetTarget(targetTransform);
        }
        */


        //this line need to change because it prevent the object ot rotate upward if it didnt see enemy 22/8/2020
        ////here is making the turret direction x face 0 after shoot
        //if (transform.rotation.x != 0 && m_target == null)
        //{
        //    transform.rotation = Quaternion.Euler(0f, m_rotation.y, 0f);
        //}

        //float maximumDistance = float.MaxValue;

        //float distance = Vector3.Distance(gameObject.transform.position, border.transform.position);
        //if (distance < maximumDistance)
        //{
        //    moveTargetPosition = border.transform;
        //}
        #endregion


        #region thing dont need
        //if (m_distance < 5 && m_distance > 1)
        //{
        //    rb.velocity = Vector3.zero;
        //    m_target = null;
        //    Debug.Log(m_distance + " the distance suppose to be less than 5 and the turret movement is set to zero");
        //}
        #endregion

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 10)
        {
            targetTransform = null;
            m_moveTowardTarget.SetTarget(targetTransform);
            Debug.Log("set back to the player object");
            m_moveTowardTarget.SetBackTarget();
            to_rotate = true;
        }
        
    }


    //private void OnTriggerExit(Collider other)
    //{
    //    onDetect = true;
    //}


    IEnumerator BeginUp()
    {
        isDown = true;
        yield return new WaitForSeconds(5f);
        isDown = false;
        isUp = false;
    }

    IEnumerator BeginDown()
    {
        isUp = true;
        yield return new WaitForSeconds(3f);
        isUp = false;
        isDown = false;
    }

    //determined where to move, use raycast to check if got anything blocking it path

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

        if (rotateLeftOrRight == 2 || rotateLeftOrRight == 3)
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
        Gizmos.color = Color.green;
        Gizmos.DrawRay(from: transform.position, direction: (transform.forward) * raycastMaxDistance);

        if (Physics.BoxCast(center: transform.position, halfExtents: transform.lossyScale / 2, direction: transform.forward,
            out hit, transform.rotation, maxDistance/2, layerMask))
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(center: transform.position + transform.forward * hit.distance, size: transform.lossyScale);
        }
        //else
        //{
        //    Gizmos.color = Color.red;
        //    Gizmos.DrawRay(from: transform.position, direction: (transform.forward) * maxDistance);
        //}

        if (Physics.BoxCast(center: transform.position, halfExtents: transform.lossyScale / 2, direction: transform.forward,
           out secondHit, transform.rotation, raycastMaxDistance, layerMask1))
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(center: transform.position + transform.forward * secondHit.distance, size: transform.lossyScale);
        }
        
    }







    //public void SetDistance(float aiDistance)
    //{
    //    m_distance = aiDistance;
    //}






    //public void SetFly(bool fly)
    //{
    //    m_isUp = fly;
    //}

    public void SetRotation(Vector3 rotate)
    {
        m_rotation = rotate;
    }


    public void SetTarget(Transform target)
    {
        m_target = target;
    }


}






//  22/7/2020 this is for more undestand but this put aside first
//if flying when got obstacle,when got obstacle what will hapen
//it will also look up and down
//before it move, it will look
//Ai need to make decision, where to move, will raycast to that direction if hit something than bouce the raycast to another direction
//the AI it will also consider to move up and down, if the AI is almost collide with another object it will slide along it, the AI will have another collider
//NExt Task when the AI raycast is on the object but when it got object blocking it path it will bounce to another place


// 22/7/2020 Finish this first need to check at 29/7/2020
//Make AI look 360;
// make AI avoid collide with walls
// make AI avoid corner by chosing suppoert navigation area(object)





//29/7/2020 finish by 5 August 2020
/*
  another solution, it require designer.
  instead of using, 
  find the nearest hitbox to the target position, it will straight move to the target position,
  the step will be ask the obstacle where is the nearest hitbox, the obstacle will check 
  where the AI position, it will generate the position where there is a collsion
  find where is the nearest hitbox and move there(object avoi)
*/
// have the object move/fly up and down when choose random position it also choose 360' direction
// when the Ai choose something below the lowest fly high limit than it will reset back to the value
//dont use the raycast bounce, finish this 2 first
