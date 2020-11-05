using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public bool isStart;
    public bool isQuit;

 
    void OnMouseUp()
    {
        if (isStart)
        {
            SceneManager.LoadScene(1);
            GetComponent<Renderer>().material.color = Color.cyan;
        }
        if (isQuit)
        {
            Application.Quit();
            GetComponent<Renderer>().material.color = Color.cyan;
        }
    }
  
}
