using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCast : MonoBehaviour
{
    /*
    public float maxDistance = 5f;
    public bool isHit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
    
    private void OnDrawGizmos()
    {
        RaycastHit raycastHit;
        isHit = Physics.BoxCast(transform.position, transform.lossyScale / 2, transform.forward
           , out raycastHit, transform.rotation, maxDistance);

        //isHit = Physics.SphereCast(transform.position, transform.lossyScale.x / 2, 
        //    transform.right, out raycastHit, maxDistance);

        if (isHit)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, transform.forward * raycastHit.distance);
            //Gizmos.DrawRay(transform.position, transform.right * raycastHit.distance);

            Gizmos.DrawWireCube(transform.position + transform.forward * raycastHit.distance, transform.lossyScale);
         
        }
        else
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position, transform.forward * maxDistance);
        }
    }

    */
}
