using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveScript : MonoBehaviour
{
    public isBurning rope; //reference for rope to check if burnin
    public float moveSpeed;
    public Transform endPos;

    // Update is called once per frame
    void Update()
    {
        if (rope.burning == true)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, endPos.position, Time.deltaTime * moveSpeed);
        }
    }
}
