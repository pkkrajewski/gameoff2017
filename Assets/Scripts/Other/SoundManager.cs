using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public SoundPack[] soundPacks;

	void Awake ()
    {
        foreach(SoundPack s in soundPacks)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.volume = s.volume;
        }
	}
	
	public void Play(string name)
    {
        bool soundFound = false;

        foreach(SoundPack s in soundPacks)
        {
            if (s.name == name)
            {
                s.Play();
                soundFound = true;
                break;
            }
        }

        //if (!soundFound)
        //    Debug.LogError("SoundManager doesn't have sound: " + name);
    }
}
