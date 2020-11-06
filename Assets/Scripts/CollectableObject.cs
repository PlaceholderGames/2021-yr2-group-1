using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.GameCenter;

public class CollectableObject : MonoBehaviour
{
    //Variables for checking if collection is allowed
    public bool isCollected;
    public bool collectionAllowed;

    //GameObjects for UI elements
    public GameObject dot;
    public GameObject cross;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Checks if collection is allowed, the collectable hasn't been collected yet and the right mouse button is pressed
        if (collectionAllowed == true && isCollected == false && Input.GetMouseButtonDown(1))
        {
            GameControl.control.noCollected += 1;
            //Toggles isCollected to prevent function from happening again
            isCollected = true;
            cross.SetActive(false);
            dot.SetActive(true);
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        //Checks if the player is within the collision trigger of the object and that the collectable hasn;t been collected yet
        if (collision.gameObject.name.Equals("playerCharacter") && isCollected == false)
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
