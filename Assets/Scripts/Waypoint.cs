using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField]
    protected float debugDrawRadius = 1.0f;

    public virtual void OnDrawGizmoz()
    {
        //draw red wire sphere at position of object
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, debugDrawRadius);
    }
}
