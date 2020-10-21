using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour, IDamageable
{
    [SerializeField]private int maxHealth = 10;
    [SerializeField] private int health;

    private void Start()
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
