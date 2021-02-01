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
        roomCompletion1.text = 1 + "- \n" + GameControl.control.noCollected[0] + " / " + GameControl.control.roomCollectables[0] + "\n" + GameControl.control.roomPercentage[0] + "\n"
        + 2 + "- \n" + GameControl.control.noCollected[1] + " / " + GameControl.control.roomCollectables[1] + "\n" + GameControl.control.roomPercentage[1] + "\n"
        + 3 + "- \n" + GameControl.control.noCollected[2] + " / " + GameControl.control.roomCollectables[2] + "\n" + GameControl.control.roomPercentage[2] + "\n";
    }
}
