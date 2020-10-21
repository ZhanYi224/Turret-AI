using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComposeTarget : MonoBehaviour, ITarget
{
    [Header("Changable Attribute")]
    [SerializeField] private GameObject targetableGameObjects;

    //private GameObject[] targetableObject;
    //use other way to save it
    public void TurretTarget()
    {
        //targetableGameObjects = this.gameObject;

        //for (int i = 0; i < targetableGameObjects.Length; i++)
        //{
        //    GameObject targetableGameobject = targetableGameObjects[i];
        //    ITarget targetable = targetableGameobject.GetComponent<ITarget>();
        //    if (targetable == null)
        //    {
        //        return;
        //    }
        //    targetable.TurretTarget();
        //}

    }
}
