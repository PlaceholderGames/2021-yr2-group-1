using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CompletionText : MonoBehaviour
{
    public TextMeshProUGUI percentage;

    void Start()
    {
        percentage = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        percentage.text = "% - \n" + GameControl.control.collectionPercentage;
    }
}
