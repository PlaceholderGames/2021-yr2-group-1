using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Killbox : MonoBehaviour
{
    public GameObject KillboxMenu;
    
    

   
    // This when attatched to an object, causes a scene change based on the number where the 0 is
    void OnTriggerEnter(Collider other)
    {
       
        //Makes cursor visible and unlocks
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        KillboxMenu.SetActive(true);
        //Pauses time within engine
        Time.timeScale = 0f;
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void loadLevel1()
    {
        
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        KillboxMenu.SetActive(false);
        //Sets time to tick at normal speed
        Time.timeScale = 1f;
       // SceneManager.LoadScene(1);
        
    }
}
