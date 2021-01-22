using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustScript : MonoBehaviour
{
    void OnGUI()
    {
        if (Time.timeScale == 0f)
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
                GameControl.control.Save();
            }
            if (GUI.Button(new Rect(10, 300, 100, 30), "Load"))
            {
                GameControl.control.Load();
            }
        }
    }
}
