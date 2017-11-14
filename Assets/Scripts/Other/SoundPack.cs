using UnityEngine;

[System.Serializable]
public class SoundPack
{
    public string name;

    public AudioClip[] clips;

    [Range(0f, 1f)]
    public float volume;

    [HideInInspector]
    public AudioSource source;

    public void Play()
    {
        int randomIndex = Random.Range(0, clips.Length);
        source.clip = clips[randomIndex];
        source.Play();
    }
}