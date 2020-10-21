using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitableObject : MonoBehaviour, IDamageable
{
    [Header("Changable attribute")]
    public int maxHealth;

    [Header("Show casing attribute")]
    public int health;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    public void damageable(int damage)
    {
        health -= damage;
        if (health == 0)
        {
            Destroy(this.gameObject);
        }
    }
}
