using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
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

        //Checks if player is on the ground
        if (controller.isGrounded)
        {
            //Checks for jump input then performs jump
            if (Input.GetButtonDown("Jump"))
            {
                verticalVelocity = jumpForce;
            }
        }

        else
        {
            //Gradually takes away gravity from verticalVelocity on jump
            verticalVelocity -= gravityScale * Time.deltaTime;
        }

        //Sets y value to value of verticalVelocity
        moveDirection.y = verticalVelocity;

        //Checks if shift is held and changes speed
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            moveSpeed = sprintSpeed;
        }

        //Checks if shift is not held and reverts speed to default
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            moveSpeed = 5.0f;
        }

        //Checks if torchToggles is off and sets to true
        if (torchToggle == false)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                torchToggle = true;
                torch.SetActive(true);
            }
        }
        //Checks if torchToggles is on and sets to false
        else if (torchToggle == true)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                torchToggle = false;
                torch.SetActive(false);
            }
        }

        //Sends input to controller for movement
        controller.Move(moveDirection * Time.deltaTime);
    }
}
