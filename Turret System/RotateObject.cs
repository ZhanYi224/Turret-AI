using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    [Header("Changable Attribute")]
    public float turretRotateSpeed = 10f;
    private Transform m_target = null;
    
    
    [Header("UNITY Configuration Field")]
    public Transform partToRotate;
    Shooting m_shooting;
    WanderAIForShooting m_wanderAI;
    private Vector3 dir;
    public Vector3 temporary_dir;

    // Start is called before the first frame update
    void Start()
    {
        m_shooting = GetComponent<Shooting>();
        m_wanderAI = GetComponent<WanderAIForShooting>();
    }

    // Update is called once per frame
    void Update()
    {
        
        //FindTargetPlayer();   
        if (m_target == null)
        {
            //if (rotation.x != 0)
            //{
            //    Debug.Log("set x to 0");
            //    transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            //}
            return;
        }
        

        //rotate the turret body to aim at target position
        dir = m_target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turretRotateSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(rotation.x, rotation.y, rotation.z);//i change here to test to make the turret go 360




        if (m_target.gameObject.layer == 9)
        {
            Debug.Log("do the shooting");
            m_shooting.ShotProcess();
        }
        m_wanderAI.SetRotation(rotation);
        

    }
    
    //take the wanderingAI, talk the collider and find the collider hitbox, hitbox know where the nearest position and assign back to himself


    //public void FixedRotate()
    //{
    //    partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    //}


    public void SetTarget(Transform target)
    {
        m_target = target;
    }


    public void SetTempDirection(Vector3 temp_dir)
    {
        temporary_dir = temp_dir;
        StartCoroutine("BeginLookForDirection");
        
    }


    IEnumerator BeginLookForDirection()
    {
        Quaternion lookAt = Quaternion.LookRotation(temporary_dir);
        while (true)
        {      
            Debug.Log("rotate");
            Vector3 temp_rotation = Quaternion.Lerp(partToRotate.rotation, lookAt, Time.deltaTime * turretRotateSpeed).eulerAngles;
            //Debug.Log(temp_rotation);
            partToRotate.rotation = Quaternion.Euler(temp_rotation.x, temp_rotation.y, temp_rotation.z);
            yield return new WaitForSeconds(1f);
        }
        
    }


}
