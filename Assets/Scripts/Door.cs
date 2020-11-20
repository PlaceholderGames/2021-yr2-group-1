using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool isOpen;
    public bool isOpenable;
    public GameObject door;
    private float currentRotation;

    void Start()
    {
        currentRotation = door.transform.eulerAngles.y + 90;
    }
    void OnTriggerEnter(Collider collision)
    {
        //Checks if the player is within the collision trigger of the object
        if (collision.gameObject.name.Equals("playerCharacter"))
        {
            isOpenable = true;
        }
    }

    void Update()
    {
        if (isOpenable == true && isOpen == false && Input.GetMouseButton(1))
        {
            isOpen = true;
            //Moves the door by 5 units on Z
            door.transform.position += new Vector3(0, 0, 5);
            //Rotates the door 90 degrees based on the current rotation
            door.transform.rotation = Quaternion.Euler(0, currentRotation, 0);
        }
    }
}
