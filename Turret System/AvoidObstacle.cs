using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoidObstacle : MonoBehaviour
{
    public GameObject[] hitBoxGo;
    public Transform turretAI;
    //public float dirNum;

    private float distance;


    WanderAIForShooting wanderAIForShooting;
    //public float speed = 3;
    // Start is called before the first frame update
    void Start()
    {
        wanderAIForShooting = turretAI.GetComponent<WanderAIForShooting>();

    }

    // Update is called once per frame
    void Update()
    {
        /*
         * from the turretAI side it will ask the wall where is the nearest collider
         * the Wall~obstacle will check where the AI position is, than it will generate the
         * position to the TurretAI and tell the TurretAI where there is a nearest collision hitbox
         * than the turret will move to that collider
         * and move pass the collider.
         */



    }

    //float AngleDir(Vector3 fwd, Vector3 targetDir, Vector3 right)
    //{
    //    Vector3 perp = Vector3.Cross(fwd,targetDir);
    //    float dir = Vector3.Dot(perp, right);

    //    if (dir > 0f)
    //    {
    //        Debug.Log("go right");
    //        return 1f;
            
    //    }else if (dir < 0f)
    //    {
    //        Debug.Log("Go left");
    //        return -1f;
    //    }
    //    else
    //    {
    //        Debug.Log("in the middle");
    //        return 0f;
    //    }
    //}


    //private void OnTriggerEnter(Collider other)
    //{
    //    if(other.gameObject == hitBoxGo[0])
    //    {
    //        Debug.Log("tell the turret go to the nearest path");
    //    }

    //    if (other.gameObject == hitBoxGo[1])
    //    {
    //        Debug.Log("go another path");
    //    }
    //}

    public Transform DetectTurret(Vector3 aiPosition)
    {
        Transform targetPosition = null;
        //check the turretAI position than let it know where is the nearest hitbox
        //transform.position
        float nearestDistance = float.MaxValue;
        //Transform nearestPoint = null;

        for (int i = 0; i < hitBoxGo.Length; i++)
        {
            Vector3 difference = new Vector3(aiPosition.x - hitBoxGo[i].transform.position.x,
            aiPosition.y - hitBoxGo[i].transform.position.y,
            aiPosition.z - hitBoxGo[i].transform.position.z);

            distance = Mathf.Pow(difference.x, 2f) + Mathf.Pow(difference.y, 2f) + Mathf.Pow(difference.z, 2f);
           
            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                targetPosition = hitBoxGo[i].transform;
            }    
           
        }
        return targetPosition;
    }







}
