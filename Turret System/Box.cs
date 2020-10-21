using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour, IDamageable
{
    public void damageable(int damage)
    {
        Destroy(this.gameObject);
    }

}
