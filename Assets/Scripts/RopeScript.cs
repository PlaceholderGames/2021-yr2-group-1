using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeScript : MonoBehaviour
{
    public int destroyTime = 1;

    //Detect collisions between the GameObjects with Colliders attached
    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.name == "Torch")
        {
            Destroy(gameObject, destroyTime);
        }
    }
}
