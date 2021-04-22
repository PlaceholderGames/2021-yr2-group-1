using UnityEngine.Audio;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class AudioControl : MonoBehaviour
{

    public Sound[] sounds;

    public static AudioControl instance;

    private Scene currentScene;
    private string isPlaying;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }

        currentScene = SceneManager.GetActiveScene();
    }

    void Start ()
    {
        Play("Theme");
    }

    void Update ()
    {
        if (currentScene != SceneManager.GetActiveScene())
        {
            currentScene = SceneManager.GetActiveScene();
            switch (currentScene.name)
            {
                case "Camp.lvl":
                    if (isPlaying != "Theme")
                    {
                        Stop(isPlaying);
                        Play("Theme");
                    }
                    break;
                case "K_Temple_Room 1":
                    Stop(isPlaying);
                    Play("Kieran");
                    break;
                case "DeansLvl":
                    Stop(isPlaying);
                    Play("Dean");
                    break;
                case "Templefinal":
                    Stop(isPlaying);
                    Play("Temple");
                    break;
                case "Dragon":
                    Stop(isPlaying);
                    Play("Joe");
                    break;
                case "Room3":
                    Stop(isPlaying);
                    Play("Sam");
                    break;
                case "Credits":
                    Stop(isPlaying);
                    Play("End");
                    break;
            }
        }
    }

    public void Play (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Play();
        isPlaying = name;
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Stop();
    }
}
