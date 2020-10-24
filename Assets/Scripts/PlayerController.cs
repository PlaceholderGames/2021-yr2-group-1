﻿using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEditor.VersionControl;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Character Variables
    public float moveSpeed;
    public float sprintSpeed;
    public float jumpForce;
    public float verticalVelocity;
    public CharacterController controller;
    private Vector3 moveDirection;
    public float gravityScale;

    //animator variable
    public Animator anim;

    //Torch Variables
    public GameObject torch;
    public bool torchToggle;

    // Start is called before the first frame update
    void Start()
    {
        //Instantiates controller
        controller = GetComponent<CharacterController>();
        torch.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Allows for input of all cardinal directions and cominations of them
        moveDirection = (transform.forward * Input.GetAxis("Vertical")) + (transform.right * Input.GetAxis("Horizontal"));
        //Stops speed being doubled on diagonal input
        moveDirection = moveDirection.normalized * moveSpeed;
        //Sets Y value to verticalVelocity
        moveDirection.y = verticalVelocity;

        //Checks if player is on the ground
        if (controller.isGrounded)
        {
            //Checks if shift is held and changes speed
            if (Input.GetKey(KeyCode.LeftShift))
            {
                moveSpeed = sprintSpeed;
            }
            //Checks if shift is not held and reverts speed to default
            else if (!Input.GetKey(KeyCode.LeftShift))
            {
                moveSpeed = 5.0f;
            }

            //Checks if jump button is pressed and jumps if so
            if (Input.GetButton("Jump"))
            {
                verticalVelocity = jumpForce;
            }
            else if (!Input.GetButton("Jump"))
            {
                verticalVelocity = 0f;
            }
        }

        if (!controller.isGrounded)
        {
            //Gradually takes away gravity from verticalVelocity on jump
            verticalVelocity -= gravityScale * Time.deltaTime;
        }

        //Checks if torchToggles is off and sets to true
        if (torchToggle == false && Input.GetKeyDown(KeyCode.F))
        {
            torchToggle = true;
            torch.SetActive(true);
        }
        //Checks if torchToggles is on and sets to false
        else if (torchToggle == true && Input.GetKeyDown(KeyCode.F))
        {
            torchToggle = false;
            torch.SetActive(false);
        }

        //Sends input to controller for movement
        controller.Move(moveDirection * Time.deltaTime);

        //animation updates
        //gets the player input for a & d and applies them to a float called strafe
        float strafe = Input.GetAxis("Horizontal");
        //gets the player input for w & a and applies them to a float called forward
        float forward = Input.GetAxis("Vertical");

        //setting the float parameters on the anim controller to be set by strafe and forward
        anim.SetFloat("Forward", forward);
        anim.SetFloat("Right", strafe);
    }
}
