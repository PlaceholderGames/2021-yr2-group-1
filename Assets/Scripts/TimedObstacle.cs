using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedObstacle : MonoBehaviour
{
    public GameObject toggleObject;
    private int timer;
    public int delay;

    //Variables for audio feedback
    [SerializeField]
    private AudioClip clip;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

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
                audioSource.Stop();
            }
            else if (!toggleObject.activeSelf)
            {
                toggleObject.SetActive(true);
                audioSource.PlayOneShot(clip);
            }
            timer += delay;
        }
        else
        {
            timer -= 1;
        }
    }
}
