using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breakScript : MonoBehaviour
{

    private GameObject self;

    void Awake()
    {
        self = this.gameObject;
    }

    void OnTriggerEnter(Collider player)
    {
        if (player.gameObject.name.Equals("playerCharacter"))
        {
            Destroy(self);
        }
    }
}
