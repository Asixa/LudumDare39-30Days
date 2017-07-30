using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animation))]
public class AnimationHelp : MonoBehaviour
{   [HideInInspector]
    public Animation anim;
    void Awake()
    {
        anim = GetComponent<Animation>();
    }

    public void play(AnimationClip clip)
    {
        anim.Stop();
        anim.AddClip(clip,clip.name);
        anim.clip = clip;
        anim.Play();
    }
}
