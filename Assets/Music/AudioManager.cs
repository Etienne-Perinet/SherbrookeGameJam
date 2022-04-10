using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;

    public static AudioManager instance;

    void Awake()
    {

        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
            
        DontDestroyOnLoad(gameObject);
        foreach(Sound s in sounds)
        {
            if (!s.sourceIsObject)
            {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;
                s.source.volume = s.volume;
                s.source.pitch = s.pitch;
                s.source.loop = s.loop;
                s.source.spatialBlend = s.space;
                
            }
            else
            {
                //AttributeAudioSource(s.name, gameObject.AddComponent<AudioSource>());
            }

            
        }
    }

    private void Start()
    {
        Debug.Log("Starting audio manager");
        //Play("Music");
    }

    public void UpdateAudioSourceSettings(Sound s)
    {
        s.source.clip = s.clip;
        s.source.volume = s.volume;
        s.source.pitch = s.pitch;
        s.source.loop = s.loop;
        s.source.spatialBlend = s.space;
    }

    public void setSoundVolume(string name, float value)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound : " + name + " wasn't found!");
            return;
        }
        //Debug.Log("VOLUME : " + s.volume * value);
        s.source.volume = s.volume * value;
    }

    public float getSoundVolume(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound : " + name + " wasn't found!");
            return 0;
        }
        return s.source.volume;
    }

    public void AttributeAudioSource(string name, AudioSource source)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound : " + name + " wasn't found!");
            return;
        }

        if (s.sourceIsObject)
        {
            s.source = source;
            UpdateAudioSourceSettings(s);
        }
        else
        {
            //s.source = gameObject.AddComponent<AudioSource>();
            //UpdateAudioSourceSettings(s);
        }

    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound : " + name + " wasn't found!");
            return;
        }
        Debug.Log("Source ? : " + s.name + " " + s.source);
        if (!s.source.isPlaying)
        {
            s.source.Play();
        }

    }

    
    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound : " + name + " wasn't found!");
            return;
        }
        s.source.Stop();
    }

}
