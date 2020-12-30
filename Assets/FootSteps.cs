using UnityEngine.Audio;
using UnityEngine;

public class FootSteps : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] clips;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Step()
    {
        AudioClip clip = GetRandomClip();
        audioSource.PlayOneShot(clip);
        //UnityEngine.Debug.Log("Step");
    }

    private AudioClip GetRandomClip()
    {
        return clips[UnityEngine.Random.Range(0, clips.Length)];
    }
}
