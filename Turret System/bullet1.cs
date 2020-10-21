using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet1 : MonoBehaviour
{
    [Header("Changable Attribute")]
    public float speed;
    public int bulletDamage;

    [Header("UNITY Configuration field")]
    public Rigidbody rb;




    private void Start()
    {
        rb.velocity = transform.forward * speed;

    }

    private void OnEnable()
    {
        rb.velocity = transform.forward * speed;
        transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        //Destroy(this.gameObject, 5f);
        //transform.localScale += new Vector3(0, 0.1f, 0);
        // * reset velocity, local scale, not special function
    }


    private void OnCollisionEnter(Collision collision)
    {
        GameObject hit = collision.gameObject;
        //HitableObject health = hit.GetComponent<HitableObject>();
        IDamageable damage = hit.GetComponent<IDamageable>();
        if (damage != null)
        {
            damage.damageable(bulletDamage);
        }
        //Destroy(this.gameObject,5f);//implement the object pooling(a group of thing together) method
        // 1. dont need to preset the thing, when no bullet the system will take the use bullet and shoot again
        // 2. this bullet, will pool the object, when enemy have the same type, when the enemy is destroy it bring out the same enemy so the system dont need to instantiate a new enemy
        // 3. reset to default kind of function, so it will reset evertime it destroy, so evertime get a reset,
        // 4. naming of the class and object need to rename it so other people also can understand

    }

}
