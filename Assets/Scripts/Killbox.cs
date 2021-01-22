using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Killbox : MonoBehaviour
{       
    public GameControl gameObj = null;
    public GameObject killboxMenu;
    public GameObject player;


    void Awake()
    {
       gameObj = FindObjectOfType<GameControl>();
       killboxMenu = GameObject.Find("playerCharacter/KillboxMenu/killboxCanvas");
    }

    void Update()
    {
        if (gameObj == null) gameObj = FindObjectOfType<GameControl>();
        if (killboxMenu == null) killboxMenu = GameObject.Find("playerCharacter/KillboxMenu/killboxCanvas");
    }

    // This when attatched to an object, causes a scene change based on the number where the 0 is
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Equals("playerCharacter"))
        {
            //Makes cursor visible and unlocks
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            killboxMenu.SetActive(true);
            //Pauses time within engine
            Time.timeScale = 0f;
        }
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void loadLevel1()
    {
        gameObj.Load("/autoSave.dat");
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        killboxMenu.SetActive(false);
        //Sets time to tick at normal speed
        Time.timeScale = 1f;
        
    }
}
