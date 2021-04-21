using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBlock : MonoBehaviour
{
    [SerializeField]
    private AudioClip clip;
    private GameObject player = null;

    void Awake()
    {
        player = GameObject.Find("playerCharacter");
    }

    // Update is called once per frame
    void Update()
    {
        if (GameControl.control.Rocks >= 4)
        {
            AudioSource.PlayClipAtPoint(clip, player.transform.position, 0.3f);
            Destroy(this.gameObject);
        }
    }
}
