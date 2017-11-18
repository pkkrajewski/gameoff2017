using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance = null;
    public AudioClip[] musicTracks;

    private AudioSource audioSource;
    private int trackNumber = -1;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!audioSource.isPlaying)
        {
            trackNumber++;
            if (trackNumber > musicTracks.Length - 1)
                trackNumber = 0;
            audioSource.clip = musicTracks[trackNumber];
            audioSource.Play();
        }   
    }
}
