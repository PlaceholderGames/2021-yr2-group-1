using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI2ndHalf : MonoBehaviour
{
    public TextMeshProUGUI roomCompletion2;

    // Start is called before the first frame update
    void Start()
    {
        roomCompletion2 = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = GameControl.control.firstHalf; i < GameControl.control.secondHalf; i++)
        {
            roomCompletion2.text = i + 1 + "- \n" + GameControl.control.noCollected[i] + " / " + GameControl.control.roomCollectables[i] + "\n" + GameControl.control.roomPercentage[i] + "\n";
        }
    }
}
