using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class burningDestroy : MonoBehaviour
{
    public isBurning rope;

    // Update is called once per frame
    void Update()
    {
        if (rope.burning == true) Destroy(gameObject);
    }
}
