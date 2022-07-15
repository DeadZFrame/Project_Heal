using System;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class SFX_Manager : MonoBehaviour
{
    private static bool soundMuted = false;
    public static SFX_Manager instance;
    public Sound[] sounds;

    public SoundArray soundArray;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        foreach (var s in sounds)
        {
            s.audioSource = gameObject.AddComponent<AudioSource>();
            s.audioSource.clip = s.clip;
            s.audioSource.volume = s.volume;
        }

        _audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void Play(string name)
    {
        if (!soundMuted)
        {
            var s = Array.Find(sounds, sound => sound.name == name);
            if (s == null)
            {
                Debug.LogWarning("Your sound named " + name + " does not exits");
                return;
            }
            s.audioSource.Play();
        }
    }
    
    public void Stop(string name)
    {
        if (!soundMuted)
        {
            var s = Array.Find(sounds, sound => sound.name == name);
            if (s == null)
            {
                Debug.LogWarning("Your sound named " + name + " does not exits");
                return;
            }
            s.audioSource.Stop();
        }
    }

    private AudioSource _audioSource;

    public void PlayArray(string name)
    {
        if (!soundMuted)
        {
            var array = soundArray.clips;
            if (array == null)
            {
                Debug.LogWarning("Your sound named " + name + " does not exits");
                return;
            }

            
            _audioSource.clip = array[Random.Range(0, array.Length)];
            
            _audioSource.Play();
        }
    }
}
