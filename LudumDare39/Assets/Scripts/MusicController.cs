using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicController : MonoBehaviour {

    public static MusicController instance;
    [HideInInspector]
    public AudioSource my_audio;

    public MusicController()
    {
        instance = this;
    }

    public void Awake()
    {
        my_audio = GetComponent<AudioSource>();
    }

    public void Play(AudioClip clip,AudioSource sender=null, bool loop=false)
    {
        if (sender == null)
        {
            my_audio.clip = clip;
            my_audio.Play();
            my_audio.loop = loop;
        }
        else
        {
            sender.clip = clip;
            sender.Play();
            sender.loop = loop;
        }
    }
}
