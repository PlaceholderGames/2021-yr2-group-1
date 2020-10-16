using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    public CharacterController controller;

    private Vector3 moveDirection;
    public float gravityScale;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //Allows for input of all for cardinal directions and combinations of them
        moveDirection = (transform.forward * Input.GetAxis("Vertical")) + (transform.right * Input.GetAxis("Horizontal"));
        //Stops speed being doubled on diagonal input
        moveDirection = moveDirection.normalized * moveSpeed;

        //Checks if player is on the ground
        if (controller.isGrounded)
        {
            //Sets Y momentum to 0 to prevent gravity from constantly increasing Y momentum
            moveDirection.y = 0f;

            //Checks for jump input then performs
            if (Input.GetButtonDown("Jump"))
            {
                moveDirection.y = jumpForce;
            }
        }

        //Sets Y value to gravity value
        moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale * Time.deltaTime);
        //Sends input to controller for movement
        controller.Move(moveDirection * Time.deltaTime);
    }
}
