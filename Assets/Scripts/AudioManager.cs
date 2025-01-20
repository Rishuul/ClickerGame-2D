using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour
{
    public Sounds[] sounds;
    void Awake()
    {
        foreach(Sounds s in sounds)
        {
            s.audioSource =gameObject.AddComponent<AudioSource>();
            s.audioSource.clip = s.clip;
            s.audioSource.volume = s.volume;
            s.audioSource.pitch=s.pitch;
            s.audioSource.loop = s.loop;
        }
    }

    void Start()
    {
        Play("gameMusic");
    }
    
    public void Play(string name)
    {
        Sounds s = Array.Find(sounds, sound=>sound.name == name);
        s.audioSource.Play();
        
    }
}
