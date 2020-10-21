using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Sensor : MonoBehaviour
{
    [Header("Circle Radius Changable Attribute")]
    public float range = 5f;
    public LayerMask targetLayerMask;//change to "targetLayerMask"

    [Header("UNITY Configuration field")]
    public Transform attackRange;
    public Text changeText;
    public Collider[] enableTarget;
    bool started = true;

    [Header("Debug Field")]
    public Collider[] enemyToDamage;
    //public Collider[] testingEnableTarget;
    public Transform m_target;   
    RotateObject m_rotateObject;

    ITarget target;

    private int a = 0;
    private int i = 0;
    //RaycastHit hit;
    // Start is called before the first frame update
    void Start()
    {
        m_rotateObject = GetComponent<RotateObject>();
        StartCoroutine("CalledUpdateTarget");
        //InvokeRepeating("OverlapSphereOnTarget", 0f, 0.5f);
        //use coroutine to manage the checking interval(duration between the thing happen, time bettween do thing)  
    }




    void UpdateTarget()
    {
        float shortestDistance = Mathf.Infinity;
        Collider nearestEnemy = null;

        for (i = 0; i < enableTarget.Length; i++)
        {
            for (a = 0; a < enemyToDamage.Length; a++)
            {
                if (enemyToDamage[a] == enableTarget[i])
                {
                    Collider enemy = enableTarget[i];
                    float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                    if (distanceToEnemy < shortestDistance)
                    {
                        shortestDistance = distanceToEnemy;
                        nearestEnemy = enemy;
                    }
                }
            }

        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            m_target = nearestEnemy.transform;

        }

        else
        {
            m_target = null;
            IdleText();
        }

        

            m_rotateObject.SetTarget(m_target);

        // when it shoot, before it shoot if the target is null, if null repeat the target from the start,
        //find another nearest target to shoot again
        
    }

    private void FixedUpdate()
    {
        
        //MyCollision();
        /*
        Transform nearestTurretTarget = m_target;
        if (nearestTurretTarget == null)
        {
            return;
        }
        ITarget turretTarget = nearestTurretTarget.GetComponent<ITarget>();
        if (turretTarget == null)
        {
            return;
        }
        turretTarget.TurretTarget();
        */
        Vector3 statPos = Camera.main.WorldToScreenPoint(this.transform.position);
        changeText.transform.position = statPos;
    }
    
    IEnumerator CalledUpdateTarget()
    {
        while (true)
        {
            OverlapSphereOnTarget();
            yield return new WaitForSeconds(1f);
        }
        yield return null;

        //StartCoroutine("CalledUpdateTarget");
        //lock it inside a while loop, coroutine loop timer
        //
    }
    

    public void OverlapSphereOnTarget()//naming about the class need to be clearly
    {    
        enemyToDamage = Physics.OverlapSphere(transform.position, range, targetLayerMask); //can use layer detect the layer
                                                     //enemyToDamage = Physics.OverlapSphere(transform.position, range);
        //overlapshere check the target again after shoot, after the shoot, 3sec later call overlapshere again

        //store the object into the array *i think i done it.
        // run a loop for itarget interface check each of the target in itarget inside, if nothing than forget it(need or dont need also can)
        //think about it, not use list to store the group of target *i think done already, i use Collider[] or Gameobject[]
        //all foreach change to for loop *i think done already, didnt see any other foreach statemnet
        //sensor, how the sensor detect new enemy *i think done already, just pull the object into the inspector
        //check whether the target is alive then check the next nearest target. *i think done already
        
            Collider i_target = enemyToDamage[0];  // use transform.gameObject = not checking the correct thing 

            target = i_target.GetComponent<ITarget>();
            Debug.Log(target);
            if (target != null)
            {
                for (int b = 0; b < enableTarget.Length; b++)
                {
                    enableTarget[b] = i_target;
                }

                UpdateTarget();
        }
        else
        {
            IdleText();
        }
        
              

            //target.TurretTarget();//try to write target = enableTarget[]     
            //for (int b = 0; b < enableTarget.Length; b++)
            //{
            //    enableTarget[0] = i_target;
            //    if (enableTarget[0])
            //    {

            //    }
            //}

    }


    //private void OnTriggerEnter(Collider other)
    //{
    //    GameObject i_target = other.gameObject;
    //    ITarget target = i_target.GetComponent<ITarget>();
    //    if (target != null)
    //    {
    //        target.TurretTarget();
    //    }        
    //}

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        if (started)
        {
            Gizmos.DrawWireSphere(transform.position, range);
            
            //Gizmos.DrawWireSphere(center: transform.position + transform.right * hit.distance,
            //    range);
        }
    }

    void IdleText()
    {
        changeText.text = "IDLE";
    }

    /*
    public void TurretTarget()
    {
        UpdateTarget();
    }
    */
}

// 8/7/2020 new Task
// Create a movement component, create a abstract class, that move the turret, move in a specific direction(move where the sensor face)
// create a AI manager component, if didnt detect target will move in that direction then stop and wait a while then look at where the sensor is looking than move there
// when detect the target look toward the target.
// dont use navMesh, use physic.addforce.
// how people implement AI on other people video

// can use back the same movement component in future







/*
private void Update()
{
    //if (sensorEye.GetComponent<BoxCast>().isHit == true)
    //{
    //    sensor.transform.Rotate(0f, 0f, 0f);
    //}
    //else
    //{
    //    sensor.transform.Rotate(0f, 1f, 0f);
    //}
}

// Update is called once per frame
void Update()
{
    if(sensorEye.GetComponent<Shooting>().targetIsTrue == true)
    {
        sensor.transform.Rotate(0f, 0f, 0f);
    }
    else
    {
        sensor.transform.Rotate(0f, 1f, 0f);
    }
}
*/
