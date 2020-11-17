using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    public CharacterController characterController;
    public GameObject Player;
    private bool IsTrigger = false;


    // Once collision happens disable playercontroller and set IsTrigger to True
        void OnTriggerEnter(Collider other)
        {
       
                Debug.Log("Enter");
        characterController.enabled = false;
                IsTrigger = true;

          }


    // Once collision Stopss enable playercontroller and set IsTrigger to False
    void OnTriggerExit(Collider other)
        {
        IsTrigger = false;
            Debug.Log("Exit");
        characterController.enabled = true;
        }

// Every frame check for the state of IsTrigger
    void Update()
    {
        if (IsTrigger == true)
        {
            if (Input.GetKeyDown(KeyCode.W))
                Player.transform.Translate(0, 0.2f, 0, Space.World);
        }
        
        if (IsTrigger == false)
        {
            characterController.enabled = true;
        }
    }
}

