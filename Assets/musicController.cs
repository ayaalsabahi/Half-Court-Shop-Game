using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicController : MonoBehaviour
{
    bool isOn = false;
    public AudioClip musicClip; // Drag and drop your music clip in the inspector
    private AudioSource musicSource;


    void Start()
    {
        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.clip = musicClip;
        musicSource.loop = true; 
        musicSource.Play();
        DontDestroyOnLoad(gameObject);
    }

}
