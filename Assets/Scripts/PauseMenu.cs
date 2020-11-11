using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;
    bool menuChange = false;

    public GameObject pauseMenu;
    public GameObject completionMenu;

    // Update is called once per frame
    void Update()
    {
        //Check that Escape has been pressed and performs functions based on state of isPaused variable
        if (Input.GetKeyDown(KeyCode.Escape))
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
    }

    public void resumeGame()
    {
        //Removes cursor from the screen and locks its position
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        pauseMenu.SetActive(false);
        completionMenu.SetActive(false);
        //Sets time to tick at normal speed
        Time.timeScale = 1f;
        isPaused = false;
        menuChange = false;
    }

    void pauseGame()
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

    public void quitGame()
    {
        Application.Quit();
    }

    public void loadLevel1()
    {
        SceneManager.LoadScene(1);
    }
}
