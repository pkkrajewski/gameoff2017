using UnityEngine;
using UnityEngine.UI;

public class BonusInfo : MonoBehaviour {

    const float oneCharacterDisplayingTime = 0.1f;

    float timeSinceStartedDisplaying;

    float displayingTime;

    public void Display(string text)
    {
        GetComponent<Text>().text = text;
        displayingTime = text.Length * oneCharacterDisplayingTime;

        timeSinceStartedDisplaying = 0;
    }

	void Update () {
        timeSinceStartedDisplaying += Time.deltaTime;

        if (timeSinceStartedDisplaying >= displayingTime)
            gameObject.GetComponent<Text>().text = "";
	}
}
