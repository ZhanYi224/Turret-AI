using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderAI : MonoBehaviour
{


    [Header("Debug Field")]
    public float moveSpeed = 3f;
    public float rotationSpeed = 50f;
    public Rigidbody rb;

    private bool isWandering = false;
    private bool isRotatingRight = false;
    private bool isRotatingLeft = false;
    private bool isMovingForward = false;

    private float rotationTime;
    private float rotationWaitTime;
    private float rotateLeftOrRight;
    private float moveThanWait;
    private float timeTomove;

    private Coroutine wanderCoroutine = null;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    { 
        if (isWandering == false)
        {
            
            if (wanderCoroutine == null)
            {
                //Debug.Log(wanderCoroutine);
                wanderCoroutine = StartCoroutine("Wander");
                //Debug.Log("here is wander coroutine when it is null also checking is coroutine is " +wanderCoroutine);
                
            }
            else
            {
                StopCoroutine(wanderCoroutine);
                //Debug.Log("Here suppose to show " + wanderCoroutine);
                wanderCoroutine = null;
            }




            //coroutine is still active when it was false
            //when it call again it create a new coroutine

            //it stop the coroutine when use and it set the cororutine to null
            //Debug.Log("here suppose to stop coroutine");



        }
        if (isRotatingRight == true)
        {
            transform.Rotate(transform.up * Time.deltaTime * rotationSpeed);
            

        }
        

        if(isRotatingLeft == true)
        {
            transform.Rotate(transform.up * Time.deltaTime * -rotationSpeed);

        }
        

        if (isMovingForward == true)
        {
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }
        //15/7/2020
        //have to use force to manage it, movement and rotating use velocity to control
        // when addforce, stop rotating at that position use drag or set velocity to 0
        //use physic and velocity.
        //make the object can fly in the air in next task
        // look for position in the air left and right in the next task
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 10)
        {
            this.transform.Rotate(transform.rotation.x, transform.rotation.y - 90, transform.rotation.z);
        }
    }


    IEnumerator Wander()
    {
        rotationTime = Random.Range(1,3);//it a function, each time call it will cause more memory. 
        rotationWaitTime = Random.Range(1,4);
        rotateLeftOrRight = Random.Range(0, 3);
        moveThanWait = Random.Range(1, 3);
        timeTomove = Random.Range(1, 5);
        //use random.seed (when call random number it will call from somewhere byt use seed it will pre-generate already and use it) on doing the Random.Range
        isWandering = true;

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
        isWandering = false;
    }





}