using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class musicController : MonoBehaviour
{
    public static musicController instance;

    bool isOn = false;
    public AudioClip musicClip; // Drag and drop your music clip in the inspector
    private AudioSource musicSource;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    void Start()
    {
        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.clip = musicClip;
        musicSource.loop = true; 
        musicSource.Play();
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;

        if (currentSceneName.Equals("Kitchen"))
        {
            musicSource.volume = 0.25f;
        }
    }

}
