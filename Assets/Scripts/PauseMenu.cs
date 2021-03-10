using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool isPaused = false;
    bool menuChange = false;

    public GameObject pauseMenu;
    public GameObject completionMenu;
    public GameObject completionCheck;

    // Update is called once per frame
    void Update()
    {
        //Check that Escape has been pressed and performs functions based on state of isPaused variable
        if (Input.GetKeyDown(KeyCode.Escape) && GameControl.control.isDead == false)
        {
            if (isPaused == true)
            {
                resumeGame();
            }
            else if (isPaused == false)
            {
                pauseGame();
            }
        }
        if (GameControl.control.templeDiscovered == false && GameControl.control.roomNumber == 2)
        {
            GameControl.control.templeDiscovered = true;
        }
        if (GameControl.control.previousRoomNumber != GameControl.control.roomNumber)
        {
            if (GameControl.control.templeDiscovered == true && GameControl.control.roomNumber != 2 && GameControl.control.noCollected[GameControl.control.roomNumber] == GameControl.control.roomCollectables[GameControl.control.roomNumber])
            {
                pauseCompletionCheck();
                UnityEngine.Debug.Log("Check worked");
            }
            GameControl.control.previousRoomNumber = GameControl.control.roomNumber;
        }
    }
    public void pauseCompletionCheck()
    {
        //Makes cursor visible and unlocks
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        completionCheck.SetActive(true);
        //Pauses time within engine
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void resumeGame()
    {
        //Removes cursor from the screen and locks its position
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        pauseMenu.SetActive(false);
        completionMenu.SetActive(false);
        completionCheck.SetActive(false);
        //Sets time to tick at normal speed
        Time.timeScale = 1f;
        isPaused = false;
        menuChange = false;
    }

    public void pauseGame()
    {
        //Makes cursor visible and unlocks
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        pauseMenu.SetActive(true);
        //Pauses time within engine
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void completionMenuChange()
    {
        if (menuChange == false)
        {
            completionMenu.SetActive(true);
            pauseMenu.SetActive(false);
            menuChange = true;
        }
        else if (menuChange == true)
        {
            pauseMenu.SetActive(true);
            completionMenu.SetActive(false);
            menuChange = false;
        }
    }

    public void saveGame()
    {
        GameControl.control.Save("/playerInfo.dat");
    }

    public void loadGame()
    {
        GameControl.control.Load("/playerInfo.dat");
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void loadLevel()
    {
        SceneManager.LoadScene(3);
        resumeGame();
    }
}
