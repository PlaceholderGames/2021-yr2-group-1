using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UICurrentLevel : MonoBehaviour
{
    public TextMeshProUGUI UIText;

    // Start is called before the first frame update
    void Start()
    {
        UIText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0 && SceneManager.GetActiveScene().buildIndex != 7)
        {
            UIText.text = (GameControl.control.roomNumber + 1) + "\n" + GameControl.control.noCollected[GameControl.control.roomNumber] + " / " +
            GameControl.control.roomCollectables[GameControl.control.roomNumber];
        }
    }
}
