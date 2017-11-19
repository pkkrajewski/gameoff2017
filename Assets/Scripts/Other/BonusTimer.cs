using UnityEngine;
using UnityEngine.UI;

public class BonusTimer : MonoBehaviour {

    const float MinTime = 5.0f;
    const float MaxTime = 15.0f;

    Text text;

    float leftTime;

    bool counting;

    public void StartCounting()
    {
        leftTime = Random.Range(MinTime, MaxTime);
        counting = true;
    }

    void Awake()
    {
        text = GetComponent<Text>();
    }
	
	void Update () {
        if (counting)
        {
            leftTime -= Time.deltaTime;

            if (Mathf.Floor(leftTime) <= 0)
            {
                Bonus.activeBonusName = "";
                text.text = "";
                counting = false;
            }
            else
                text.text = "Bonus: " + Mathf.Floor(leftTime).ToString();
        }
	}
}
