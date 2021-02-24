using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingOpenScript : MonoBehaviour
{
    public GameControl gameObj = null; //reference to game control

    void Awake()
    {
        gameObj = FindObjectOfType<GameControl>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObj == null) gameObj = FindObjectOfType<GameControl>();
        if (gameObj.collectionPercentage >= 80) Destroy(gameObject);
    }
}
