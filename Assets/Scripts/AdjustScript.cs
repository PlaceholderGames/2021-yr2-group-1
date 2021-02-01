using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustScript : MonoBehaviour
{
    public PauseMenu pauseMenu;

    void Awake()
    {
        GameObject canvas = GameObject.Find("Canvas");
        pauseMenu = canvas.GetComponent<PauseMenu>();
    }

    void OnGUI()
    {
        if (pauseMenu.isPaused)
        {
            // if(GUI.Button(new Rect(10,100,210,30), "Collection Percentage Up"))
            // {
            //     GameControl.control.collectionPercentage += 10;
            // }
            // if (GUI.Button(new Rect(10, 140, 210, 30), "Collection Percentage Down"))
            // {
            //     GameControl.control.collectionPercentage -= 10;
            // }
            if (GUI.Button(new Rect(10, 260, 100, 30), "Save"))
            {
                GameControl.control.Save("/playerInfo.dat");
            }
            if (GUI.Button(new Rect(10, 300, 100, 30), "Load"))
            {
                GameControl.control.Load("/playerInfo.dat");
            }
        }
    }
}
