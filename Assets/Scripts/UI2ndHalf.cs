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
        roomCompletion2.text = 3 + "- \n" + GameControl.control.noCollected[2] + " / " + GameControl.control.roomCollectables[2] + "\n" + GameControl.control.roomPercentage[2] + "\n"
        + 4 + "- \n" + GameControl.control.noCollected[3] + " / " + GameControl.control.roomCollectables[3] + "\n" + GameControl.control.roomPercentage[3] + "\n";
    }
}
