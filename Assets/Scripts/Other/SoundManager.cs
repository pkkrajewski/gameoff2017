using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public Sound[] sounds;

	void Awake ()
    {
        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
        }
	}
	
	public void Play(string name)
    {
        bool soundFound = false;

        foreach(Sound s in sounds)
        {
            if (s.name == name)
            {
                s.source.Play();
                soundFound = true;
                break;
            }
        }

        if (!soundFound)
            Debug.LogError("SoundManager doesn't have sound: " + name);
    }
}
