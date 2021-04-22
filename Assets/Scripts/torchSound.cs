using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class torchSound : MonoBehaviour
{
    //Variables for audio feedback
    [SerializeField]
    private AudioClip clip;
    public AudioSource audioSource;
    private GameObject torch;
    private bool toggle = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        torch = GameObject.Find("Torch");
        audioSource.clip = clip;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(toggle);

        if (torch.activeInHierarchy)
        {
            if (toggle == false)
            {
                Debug.Log("active torch");
                audioSource.PlayOneShot(clip);
                toggle = true;
            }
        }
        else
        {
            toggle = false;
            audioSource.Stop();
        }

    }
}
