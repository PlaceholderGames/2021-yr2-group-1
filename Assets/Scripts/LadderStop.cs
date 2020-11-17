using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderStop : MonoBehaviour
{
    public GameObject Player;
    public CharacterController playerController;
    private bool IsTrigger = false;

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            playerController.enabled = true;
            IsTrigger = true;

        }
    }

    void Update()
    {
        if (IsTrigger == true)
        {


            Player.transform.Translate(0, 0, 0, Space.World);

        }
    }
}
