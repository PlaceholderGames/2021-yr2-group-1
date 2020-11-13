using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI1stHalf : MonoBehaviour
{
    public TextMeshProUGUI roomCompletion1;

    // Start is called before the first frame update
    void Start()
    {
        roomCompletion1 = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < GameControl.control.firstHalf; i++)
        {
            roomCompletion1.text = i + 1 + "- \n" + GameControl.control.noCollected[i] + " / " + GameControl.control.roomCollectables[i] + "\n" + GameControl.control.roomPercentage[i] + "\n";
        }
    }
}
