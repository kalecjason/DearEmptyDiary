using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMPlayer : MonoBehaviour {

    public static BGMPlayer mPlayer;
    private AudioSource source;

    // Singleton pattern to ensure only one MusicPlayer exists and keeps existing
    public void Awake()
    {
        if (!mPlayer)
        {
            mPlayer = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void Start()
    {
        // Get the audio source and play it
        this.source = GetComponent<AudioSource>();
        this.source.Play();
    }
}
