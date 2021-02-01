using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedObstacle : MonoBehaviour
{
    public GameObject toggleObject;
    private int timer;
    public int delay;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer == 0)
        {
            if (toggleObject.activeSelf)
            {
                toggleObject.SetActive(false);
            }
            else if (!toggleObject.activeSelf)
            {
                toggleObject.SetActive(true);
            }
            timer += delay;
        }
        else
        {
            timer -= 1;
        }
    }
}
