using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SensorAI : MonoBehaviour
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
    public Transform temp_target;
    //public Vector3 temp_target;
    RotateObject m_rotateObject;
    WanderAIForShooting m_wanderAiForShooting;
    Shooting m_shooting;
    ITarget target;

    private int a = 0;
    private int i = 0;
    private bool isShooting = false;
    private Vector3 temp_dir;

    float shortestDistance = Mathf.Infinity;
    Collider nearestEnemy = null;

    // Start is called before the first frame update
    void Start()
    {
        m_shooting = GetComponent<Shooting>();
        m_rotateObject = GetComponent<RotateObject>();
        m_wanderAiForShooting = GetComponent<WanderAIForShooting>();
        StartCoroutine("CalledUpdateTarget");

        
    }

    public void UpdateTarget()
    {

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
            temp_target = m_target;
            //isShooting = true;
            //temp_target = new Vector3(m_target.position.x, m_target.position.y, m_target.position.z); 
        }
        else
        {
            Debug.Log(m_target + " ? is it found");
            m_target = null;
            IdleText();
        }
        
        m_wanderAiForShooting.SetTarget(m_target);
        RotateTheTurret();
        
    }

    public void RotateTheTurret()
    {
        m_rotateObject.SetTarget(m_target);
    }


    public void SetBackTarget()
    {
        m_target = temp_target;
        //temp_dir = temp_target;
        //m_rotateObject.SetTempDirection(temp_dir);
        RotateTheTurret();
        
    }


    void FixedUpdate()
    {

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
        

        //StartCoroutine("CalledUpdateTarget");
        //lock it inside a while loop, coroutine loop timer
    }


    //this script here is run after detect the enemy in the overlapsphere
    public void OverlapSphereOnTarget()//naming about the class need to be clearly
    {
        enemyToDamage = Physics.OverlapSphere(transform.position, range, targetLayerMask);
        //turret body itself should ignore itself, firstly he need to detect if the layer is belong to him, the type of component you want, which whenever it want store it else filter out
        //in this list of enemy which is type of target to choose, filter out all not unnescary target
        // check who is the nearest
        Collider i_target = enemyToDamage[0];//this need to change
        target = i_target.GetComponent<ITarget>();
        //Debug.Log(target);
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
            m_target = null;
            IdleText();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        if (started)
        {
            Gizmos.DrawWireSphere(transform.position, range);
        }
    }

    void IdleText()
    {
        changeText.text = "IDLE";
    }


    public void SetTarget(Transform moveTowardsTarget)
    {
        m_target = moveTowardsTarget;
        RotateTheTurret();
    }


}
