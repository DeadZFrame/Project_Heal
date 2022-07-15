using System;
using UnityEngine;

[Serializable]
public class SoundArray
{
    public string name;
    public AudioClip[] clips;

    [Range(0f, 1f)]
    public float volume;

    [HideInInspector]
    public AudioSource audioSource;
}
