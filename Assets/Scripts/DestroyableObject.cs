using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableObject : MonoBehaviour
{
    public bool isDestructable;
    public bool torchRequired;

    public GameObject dot;
    public GameObject cross;

    public PlayerController playerControl;

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
        if (isDestructable == true && torchRequired == false && Input.GetMouseButton(1))
        {
            Destroy(gameObject);
            cross.SetActive(false);
            dot.SetActive(true);
        }
        else if (isDestructable == true && torchRequired == true && Input.GetMouseButton(1))
        {
            if(playerControl.torchToggle == true)
            {
                Destroy(gameObject);
                cross.SetActive(false);
                dot.SetActive(true);
            }
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        //Checks if the player is within the collision trigger of the object
        if (collision.gameObject.name.Equals("playerCharacter") && isDestructable == false)
        {
            isDestructable = true;
            cross.SetActive(true);
            dot.SetActive(false);
        }
    }

    void OnTriggerExit(Collider collision)
    {
        //Checks if the player is within the collision trigger of the object
        if (collision.gameObject.name.Equals("playerCharacter"))
        {
            isDestructable = false;
            cross.SetActive(false);
            dot.SetActive(true);
        }
    }
}
