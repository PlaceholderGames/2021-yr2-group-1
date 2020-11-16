using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    public CharacterController playerController;
    public GameObject Player;
    private bool IsTrigger = false;

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            playerController.enabled = false;
            IsTrigger = true;
        }
        
        
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Exit");
            IsTrigger = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (IsTrigger == true)
        {


            Player.transform.Translate(0, 0.01f, 0, Space.World);

        }
        if (IsTrigger == false)
        {
            playerController.enabled = true;
            Player.transform.Translate(0, 0, 0, Space.World);
        }
    }


}

