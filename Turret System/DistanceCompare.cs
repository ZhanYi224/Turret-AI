using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceCompare : IComparer
{
    private Transform compareTransform;

    public DistanceCompare(Transform compTransform)
    {
        compareTransform = compTransform;
    }

    public int Compare(object x, object y)
    {
        Collider xCollider = x as Collider;
        Collider YCollider = y as Collider;

        Vector3 offset = xCollider.transform.position - compareTransform.position;
        float xDistance = offset.sqrMagnitude;

        offset = YCollider.transform.position - compareTransform.position;
        float yDistance = offset.sqrMagnitude;

        return xDistance.CompareTo(yDistance);
    }

    
}
