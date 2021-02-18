using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.SocialPlatforms.GameCenter;

public class CollectableObject : MonoBehaviour
{
    //Variables for checking if collection is allowed
    public bool collectionAllowed;

    //GameObjects for UI elements
    public GameObject dot;
    public GameObject cross;

    public GameObject image; //reference for image on painting
    public Renderer rend; //for referencing mesh on image

    public int itemID;

    // Start is called before the first frame update
    void Start()
    {
        cross.SetActive(true);
        dot = GameObject.Find("dot");
        cross = GameObject.Find("cross");
        cross.SetActive(false);

        //setting up the reference to the image mesh
        rend = image.GetComponent<Renderer>();
        rend.enabled = true;
    }

        // Update is called once per frame
        void Update()
    {
        //Checks if collection is allowed, the collectable hasn't been collected yet and the right mouse button is pressed
        if (collectionAllowed == true && GameControl.control.isCollected[itemID] == false && Input.GetMouseButtonDown(1))
        {
            GameControl.control.noCollected[GameControl.control.roomNumber] += 1;
            Debug.Log("Collected - " + GameControl.control.noCollected[GameControl.control.roomNumber]);
            //Toggles isCollected to prevent function from happening again
            GameControl.control.isCollected[itemID] = true;
            Debug.Log("Collected - " + itemID + " = " + GameControl.control.isCollected[itemID]);
            cross.SetActive(false);
            dot.SetActive(true);
            GameControl.control.Save("/playerInfo.dat");
            //Destroy(image); //this will cause problems when reloading
            //rend.enabled = false;
        }

        if (GameControl.control.isCollected[itemID] == true) rend.enabled = false;
        else rend.enabled = true;
    }

    void OnTriggerEnter(Collider collision)
    {
        //Checks if the player is within the collision trigger of the object and that the collectable hasn;t been collected yet
        if (collision.gameObject.name.Equals("playerCharacter") && GameControl.control.isCollected[itemID] == false)
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
