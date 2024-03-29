﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool isOpen;
    public bool isOpenable;
    public GameObject door;
    private float currentRotation;

    public GameObject dot;
    public GameObject cross;

    void Start()
    {
        cross.SetActive(true);
        dot = GameObject.Find("dot");
        cross = GameObject.Find("cross");
        currentRotation = door.transform.eulerAngles.y + 90;
        cross.SetActive(false);
    }

    void Update()
    {
        if (isOpenable == true && isOpen == false && Input.GetMouseButton(1))
        {
            //Moves the door by 5 units on Z
            door.transform.position += new Vector3(0, 0, 5);
            //Rotates the door 90 degrees based on the current rotation
            door.transform.rotation = Quaternion.Euler(0, currentRotation, 0);
            cross.SetActive(false);
            dot.SetActive(true);
            isOpen = true;
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        //Checks if the player is within the collision trigger of the object
        if (collision.gameObject.name.Equals("playerCharacter") && isOpen == false)
        {
            isOpenable = true;
            cross.SetActive(true);
            dot.SetActive(false);
        }
    }

    void OnTriggerExit(Collider collision)
    {
        //Checks if the player is within the collision trigger of the object
        if (collision.gameObject.name.Equals("playerCharacter"))
        {
            isOpenable = false;
            cross.SetActive(false);
            dot.SetActive(true);
        }
    }
}
