using UnityEngine.Audio;
using UnityEngine;

public class FootSteps : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] clips;
    private CharacterController controller;
    private GameObject playerObj = null;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (playerObj == null) playerObj = GameObject.Find("playerCharacter");
        if (controller == null) controller = playerObj.GetComponent<CharacterController>();
    }

    public void Step()
    {
        if (controller.isGrounded)
        {
            AudioClip stepClip = GetRandomClip();
            audioSource.clip = stepClip;
            audioSource.Play();
            //UnityEngine.Debug.Log("Step");
        }
        //added in if check to only create footstep sound when the player is grounded however this doesn't always work
        //i.e. sometimes when player is walking on the ground, no footstep sound is played, implying the controller does not think the player is grounded
    }

    private AudioClip GetRandomClip()
    {
        return clips[UnityEngine.Random.Range(0, clips.Length)];
    }
}
