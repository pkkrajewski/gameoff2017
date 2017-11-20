using UnityEngine;
using UnityEngine.UI;

public class BlinkingText : MonoBehaviour {

    public Text text;

    public float interval;

    float timeSinceChangingState;

	void Start () {
        timeSinceChangingState = 0;
	}

	void Update () {
        timeSinceChangingState += Time.deltaTime;

        if(timeSinceChangingState >= interval)
        {
            text.enabled = !text.enabled;
            timeSinceChangingState = 0;
        }
	}
}
