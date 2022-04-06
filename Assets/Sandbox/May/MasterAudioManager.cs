using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MasterAudioManager : MonoBehaviour
{
    // Sound Array where all sound clips go into
    public Sound[] sounds;

    public static MasterAudioManager instance;

    // Start is called before the first frame update
    void Awake()
    {
        // Ensures there is only one Audio Manager in the scene
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        // Creates an audio source for each sound clip in the array, and gives the audio source necessary variables
        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.loop = s.loop;
        }
    }
    public void Play(string name)
    {
        // Finds the audio clip with the string name given
        Sound s = Array.Find(sounds, sound => sound.name == name);
        // Error given if name isn't found
        if (s == null)
        {
            Debug.LogWarning("Sound (" + name + ") not found. Did you misspell something?");
            return;
        }
        // Sets volume and pitch, and plays the sound clip
        s.source.volume = s.volume;
        s.source.pitch = s.pitch;
        s.source.Play();
    }

}
