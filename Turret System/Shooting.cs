using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
/*

    This code here use as turret manager

*/
public class Shooting : MonoBehaviour
{
    
    private float TimeBtwShoot;

    [Header("Turret Control Changable Attribute")]
    public float StartTimeBtwShoot;
    public GameObject Projectile;
    //public float turretRotateSpeed = 10f;

    [Header("UNITY Configuration field")]
    public GameObject spawnBulletPosition;
    private float TotalShotCount = 20; // totalShotCount
    //public GameObject sensorScriptLocation;
    private Transform m_target;
    //public Transform partToRotate;
    public Text changeText;

    //SensorAI turretAI;
   
    [Header("Debug field")]
    public float shotcount = 0; // shotCount
    public float cooldown = 0;

    //SensorAI sensorAI;
    

    //public List<GameObject> hitableObjects;
    //public int i = 2;
    //public GameObject wall;
    //public float viewDistance;
    //private Vector3 aimDirection;
    //public GameObject sensoreye;
    //public GameObject turret;
    //public bool targetIsTrue;

    // Start is called before the first frame update
    void Start()
    {
        //turretAI = GetComponent<SensorAI>();
        //InvokeRepeating("UpdateTarget", 0f, 0.5f);
        //sensorAI = GetComponent<SensorAI>();
    }


    // Update is called once per frame
    void Update()
    {
        ////FindTargetPlayer();   
        //if (m_target == null)
        //{
        //    return;
        //}

        ////rotate the turret body to aim at target position
        //Vector3 dir = m_target.position - transform.position;
        //Quaternion lookRotation = Quaternion.LookRotation(dir);
        //Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turretRotateSpeed).eulerAngles;
        //partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        
        //here call for shoot process
        //ShotProcess();
        
        //here use for change the turret stat
        Vector3 statPos = Camera.main.WorldToScreenPoint(this.transform.position);
        changeText.transform.position = statPos;

    }




    //void UpdateTarget()
    //{
    //    float shortestDistance = Mathf.Infinity;
    //    Collider nearestEnemy = null;
    //    foreach (Collider enemy in sensorScriptLocation.GetComponent<Sensor>().enemyToDamage)
    //    {
    //        float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
    //        if (distanceToEnemy < shortestDistance)
    //        {
    //            shortestDistance = distanceToEnemy;
    //            nearestEnemy = enemy;
    //        }
    //    }
    //    if (nearestEnemy != null && shortestDistance <= sensorScriptLocation.GetComponent<Sensor>().range)
    //    {
    //        m_target = nearestEnemy.transform;
    //    }
    //    else
    //    {
    //        m_target = null;
    //        IdleText();
    //    }
    //}


    public void ShotProcess()
    {
        if ( shotcount < TotalShotCount)
        {
            ShotBulletText();
            ShotBullet();
            
        }
        else
        {
            CooldownText();
            cooldown -= Time.deltaTime;
            
            if (cooldown <= 0)
            {
                shotcount = 0;
                cooldown = 0;
                
            }
            //turretAI.OverlapSphereOnTarget();
        }
        
    }

    void ShotBullet()
    {
        if (TimeBtwShoot <= 0) // shot x number of bullet
        {
            GameObject obj = ObjectPooler.current.GetPooledObject();
            if(obj == null)
            {
                return;
            }

            obj.transform.position = spawnBulletPosition.transform.position;
            obj.transform.rotation = spawnBulletPosition.transform.rotation;
            obj.SetActive(true);
            
                
            //Instantiate(Projectile, spawnBulletPosition.transform.position, spawnBulletPosition.transform.rotation);

            TimeBtwShoot = StartTimeBtwShoot;
            shotcount += 1;

            cooldown = shotcount / 5;
        }
        else
        {
            TimeBtwShoot -= Time.deltaTime;
        }
    }

   
    void ShotBulletText()
    {
        changeText.text = "Shooting(Bullet)";
    }

    void CooldownText()
    {
        changeText.text = "Cooldown";
    }

    //void IdleText()
    //{
    //    changeText.text = "IDLE";
    //}






}

/*
void FindTargetPlayer()
{
    //if wall hit the ray than turret shoot bullet


    for (int i = 0; i < sensor.GetComponent<Sensor>().enemyToDamage.Length; i++)
    {
        if (sensor.GetComponent<Sensor>().enemyToDamage[i].gameObject.GetComponent<HitableObject>())
        {
            ShotProcess();
            //FaceTarget();
        }
    }

    /*
    Vector3 PosA = transform.position;
    Vector3 posB = hitableObjects[i].transform.position;

    if (Vector3.Distance(PosA, posB) < viewDistance)
    {
        inside view radius
        Vector3 directionToWall = (posB - PosA).normalized;
        if (Vector3.Angle(aimDirection, directionToWall) < sensoreye.GetComponent<SensorEye>().viewAngle / 2f)
        {
        // inside FOV
            shotbullet();              
        }

    RaycastHit raycastHit;
    if (Physics.Raycast(sensoreye.GetComponent<BoxCast>().transform.position, sensoreye.GetComponent<BoxCast>().transform.forward,
        out raycastHit, sensoreye.GetComponent<BoxCast>().maxDistance))
    {
        if (raycastHit.collider.gameObject.GetComponent<HitableObject>())//get component and get the interface
        {
            ShotProcess();
            FaceTarget();
        }
    }

    if (Physics.Raycast(sensoreye.GetComponent<SensorEye>().transform.position, sensoreye.GetComponent<SensorEye>().transform.forward, 
        out raycastHit, sensoreye.GetComponent<SensorEye>().viewAngle ))
    {
        targetIsTrue = true;
        //when raycast touch the thing is set than run the shotprocess code
        if (raycastHit.collider.gameObject.GetComponent<HitableObject>())//get component and get the interface
        {
            ShotProcess();
            FaceTarget();
        }

    }
    else
    {
        targetIsTrue = false;
    }
    }
}*/



/*
void FaceTarget()
{

   Vector3 direction = (-target.position) - (-transform.position);
   //Vector3 direction = (sensor.GetComponent<Sensor>().enemyToDamage[i].transform.position) - (transform.position);
   Quaternion rotation = Quaternion.LookRotation(direction);
   transform.rotation = Quaternion.Lerp(transform.rotation, rotation, turretRotateSpeed * Time.deltaTime);


}
*/



/*
void ShotBullet()//ShotBullet()
{

if (TimeBtwShoot <= 0 && shotcount < TotalShotCount) // shot x number of bullet
{
    Instantiate(Projectile, spawnBulletPosition.transform.position, spawnBulletPosition.transform.rotation);

    TimeBtwShoot = StartTimeBtwShoot;
        shotcount += 1;

        cooldown = shotcount / 5;   
}
else
{
    TimeBtwShoot -= Time.deltaTime;
}

if(shotcount >= 20) //if shotbullet reach x amount go cooldown
{
    cooldown -= Time.deltaTime;
    if(cooldown <= 0)
    {
        shotcount = 0;
        cooldown = 0;
    }       
}
}
*/
