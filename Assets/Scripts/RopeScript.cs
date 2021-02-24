using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeScript : MonoBehaviour
{
    public int destroyTime = 1;
    //public GameObject above;
    //public GameObject below;
    public GameObject[] allAbove;
    public isBurning rope;

    //Detect collisions between the GameObjects with Colliders attached
    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.name == "Torch")
        {
            Destroy(gameObject, destroyTime);
            if (rope.burning == false) rope.burning = true;
            for (int i=0;i<allAbove.Length;i++) //cycle through the array and remove each item 1 second at a time
            {
                //add in material change
                Destroy(allAbove[i], (destroyTime + i));
            }
            //if (above != null) Destroy(above, destroyTime);
            //if (below != null) Destroy(below, destroyTime);
            //Debug.Log(this.transform.parent.gameObject.name);
            //Destroy(this.transform.parent.gameObject, (destroyTime*3));
        }
    }
}

//would be good to set up an array which it passes through recursively
//BG_lava