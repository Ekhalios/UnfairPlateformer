using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public AudioClip[] clips;
    public AudioSource audioSource;
    private TextSlider volume;
    void Start()
    {
        volume = GetComponent<TextSlider>();
        audioSource.loop = false;
    }

    private AudioClip GetClip()
    {
        return clips[Random.Range(0, clips.Length)];
    }

    void Update()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.clip = GetClip();
            audioSource.Play();
        }
    }
}
