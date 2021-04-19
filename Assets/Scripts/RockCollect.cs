using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockCollect : MonoBehaviour
{
    //Variables for audio feedback
    [SerializeField]
    private AudioClip clip;
    private AudioSource audioSource;

    //Variables for checking if collection is allowed
    public bool collectionAllowed;

//GameObjects for UI elements
public GameObject dot;
public GameObject cross;

public int itemID;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        cross.SetActive(true);
        dot = GameObject.Find("dot");
        cross = GameObject.Find("cross");
        cross.SetActive(false);
    
    }
    
    // Update is called once per frame
    void Update()
    {
        //Checks if collection is allowed, the collectable hasn't been collected yet and the right mouse button is pressed
        if (collectionAllowed == true && Input.GetMouseButtonDown(1))
        {
           
            //Toggles isCollected to prevent function from happening again
            Debug.Log("Collected - " + itemID);
            audioSource.PlayOneShot(clip);
            cross.SetActive(false);
            dot.SetActive(true);  
            GameControl.control.Rocks += 1;
            Debug.Log(GameControl.control.Rocks);
                Destroy(gameObject);
            
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
