using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
//using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //Character Variables
    public float moveSpeed;
    public float sprintSpeed;
    public float jumpForce;
    public float verticalVelocity;
    public CharacterController controller;
    public Vector3 moveDirection;
    public float gravityScale;

    //Ladder Variables
    public bool ladderCollision;
    public bool ladderToggle;

    //animator variable
    public Animator anim;

    //Jump variable
    public bool isJumping;

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

    //Ladder Collision Checks
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ladder")
        {
            ladderCollision = true;
            //ladderToggle = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ladder")
        {
            ladderCollision = false;
            ladderToggle = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift)) //implementation of level skip function for testing purposes
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
                SceneManager.LoadScene(1);
                UnityEngine.Debug.Log("scene 1 skip");
            }
            if (Input.GetKeyDown(KeyCode.F2))
            {
                SceneManager.LoadScene(2);
                UnityEngine.Debug.Log("scene 2 skip");
            }
            if (Input.GetKeyDown(KeyCode.F3))
            {
                SceneManager.LoadScene(3);
                UnityEngine.Debug.Log("scene 3 skip");
            }
            if (Input.GetKeyDown(KeyCode.F4))
            {
                SceneManager.LoadScene(4);
                UnityEngine.Debug.Log("scene 4 skip");
            }
            if (Input.GetKeyDown(KeyCode.F5))
            {
                SceneManager.LoadScene(5);
                UnityEngine.Debug.Log("scene 5 skip");
            }
            if (Input.GetKeyDown(KeyCode.F6))
            {
                SceneManager.LoadScene(6);
                UnityEngine.Debug.Log("scene 6 skip");
            }
        }

        if (ladderToggle == false)
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
                    isJumping = true;
                }
                else if (!Input.GetButton("Jump"))
                {
                    verticalVelocity = 0f;
                    isJumping = false;
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
        }
        if (ladderCollision == true)
        {
            if (Input.GetMouseButtonDown(1) && ladderToggle == false)
            {
                ladderToggle = true;
            }
            else if (Input.GetMouseButtonDown(1) && ladderToggle == true)
            {
                ladderToggle = false;
            }
            if (ladderToggle == true)
            {
                //Allows for input of all cardinal directions and cominations of them
                moveDirection = (transform.up * Input.GetAxis("Vertical")) + (transform.right * Input.GetAxis("Horizontal"));
                //Stops speed being doubled on diagonal input
                moveDirection = moveDirection.normalized * moveSpeed;
                //Checks if shift is held and changes speed
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    moveSpeed = sprintSpeed / 2;
                }
                //Checks if shift is not held and reverts speed to default
                else if (!Input.GetKey(KeyCode.LeftShift))
                {
                    moveSpeed = 5.0f;
                }
            }
        }

        //Sends input to controller for movement
        controller.Move(moveDirection * Time.deltaTime);

        //animation updates
        //gets the player input for a & d and applies them to a float called strafe
        float strafe = Input.GetAxis("Horizontal");
        //gets the player input for w & s and applies them to a float called forward
        float forward = Input.GetAxis("Vertical");

        float up = moveDirection.y;

        //setting the float parameters on the anim controller to be set by strafe and forward
        anim.SetFloat("Forward", forward);
        anim.SetFloat("Right", strafe);
        anim.SetFloat("Up", up);

        // add in toggle for animation update
        anim.SetBool("torchTriggered", torchToggle);
        anim.SetBool("climbingTriggered", ladderToggle);
        anim.SetBool("isJumping", isJumping);
    }
}
