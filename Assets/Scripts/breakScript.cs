using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breakScript : MonoBehaviour
{
    //Variables for audio feedback
    [SerializeField]
    private AudioClip clip;
    private GameObject self;

    void Awake()
    {
        self = this.gameObject;
    }

    void OnTriggerEnter(Collider player)
    {
        if (player.gameObject.name.Equals("playerCharacter"))
        {
            AudioSource.PlayClipAtPoint(clip, transform.position, 0.5f);
            Destroy(self);
        }
    }
}
