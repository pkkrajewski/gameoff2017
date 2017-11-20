using UnityEngine;
using UnityEngine.UI;

public class SoundMuting : MonoBehaviour
{
    public Sprite soundOn;
    public Sprite soundOff;

    private Image speakerImage;

    void Awake()
    {
        speakerImage = GetComponent<Image>();
    }

    public void OnClickSpeaker()
    {
        if(AudioListener.pause)
        {
            AudioListener.pause = false;
            speakerImage.sprite = soundOn;
        }
        else
        {
            AudioListener.pause = true;
            speakerImage.sprite = soundOff;
        }
    }
}
