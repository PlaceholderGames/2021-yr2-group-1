using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    //Variables for checking if collection is allowed
    public bool collectionAllowed;

    //GameObjects for UI elements
    public GameObject dot;
    public GameObject cross;
    public GameObject toggleObject;
    public GameObject lever;

    public float currentRotation;

    // Start is called before the first frame update
    void Start()
    {
        cross.SetActive(true);
        dot = GameObject.Find("dot");
        cross = GameObject.Find("cross");
        cross.SetActive(false);
        currentRotation = lever.transform.eulerAngles.x + 90;
    }

    // Update is called once per frame
    void Update()
    {
        //Checks if collection is allowed, the collectable hasn't been collected yet and the right mouse button is pressed
        if (collectionAllowed == true && Input.GetMouseButtonDown(1) && toggleObject.activeSelf)
        {
            toggleObject.SetActive(false);
            lever.transform.rotation = Quaternion.Euler(currentRotation, 0, 0);
            currentRotation = lever.transform.eulerAngles.x - 90;
        }
        else if (collectionAllowed == true && Input.GetMouseButtonDown(1) && !toggleObject.activeSelf)
        {
            toggleObject.SetActive(true);
            lever.transform.rotation = Quaternion.Euler(currentRotation, 0, 0);
            currentRotation = lever.transform.eulerAngles.x + 90;
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        //Checks if the player is within the collision trigger of the object and that the collectable hasn;t been collected yet
        if (collision.gameObject.name.Equals("playerCharacter"))
        {
            collectionAllowed = true;
            cross.SetActive(true);
            dot.SetActive(false);
        }
    }

    void OnTriggerExit(Collider collision)
    {
        //Checks if the player has left the collision trigger and reverts variables
        if (collision.gameObject.name.Equals("playerCharacter"))
        {
            collectionAllowed = false;
            cross.SetActive(false);
            dot.SetActive(true);
        }
    }
}