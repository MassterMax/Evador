using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    static AudioManager copy;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (copy == null)
            copy = this;
        else
            Destroy(gameObject);
    }

    AudioClip final;
    AudioClip start;
    AudioSource AS;

    public void Mute(bool value)
    {
        AS.mute = value;
    }

    public void Play()
    {
        AS.Play();
    }

    public void SetLoop(bool value)
    {
        AS.loop = value;
    }

    public void SetFinal()
    {
        AS.clip = final;
    }

    public void SetStart()
    {
        AS.clip = start;
    }

    void Start()
    {
        AS = GetComponent<AudioSource>();
        final = (AudioClip)Resources.Load("Music/final");
        start = (AudioClip)Resources.Load("Music/default");
    }

}

