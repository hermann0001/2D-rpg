using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public List<Sound> sounds;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        } else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = s.mixer;
        } 
    }

    public void Play(string sound)
    {
        if (sound == null) return;
        Sound s = sounds.Find(item => item.name == sound);  
        s.source.Play();
    }

    public void Stop(string sound)
    {
        Sound s = sounds.Find(item => item.name == sound);
        s.source.Stop();   
    }

    public void Stop()
    {
        Sound s = sounds.Find(item => item.source.isPlaying == true);
        if(s != null) s.source.Stop();
    }
}
