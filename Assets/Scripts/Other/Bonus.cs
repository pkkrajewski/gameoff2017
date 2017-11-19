using UnityEngine;

public class Bonus : MonoBehaviour
{
    [HideInInspector]
    public static string activeBonusName;

    BonusInfo bonusInfo;
    BonusTimer bonusTimer;

    static string[] bonusTitles = new string[]
    {
        "SlowerPlayerWalking",
        "FasterPlayerWalking",
        "SlowerPlayerShooting",
        "FasterPlayerShooting",
        "PlayerImmortality",
        "HealthPacks+1",
        "HealthPacks-1",
        "FasterEnemyShooting",
        "SlowerEnemyShooting",
        "FasterEnemyWalking",
        "SlowerEnemyWalking",
    };

    static string[] bonusDescriptions = new string[]
    {
        "You walk slower",
        "You walk faster",
        "You shoot slower",
        "You shoot faster",
        "You are immortal",
        "You earned one health pack :)",
        "You lost one health pack ;(",
        "Enemies shoot faster",
        "Enemies shoot slower",
        "Enemies walk faster",
        "Enemies walk slower"
    };

    static int[] bonusIndexesNotUsingTimer = new int[] { 5, 6 };

    void Awake()
    {
        bonusInfo = GameObject.Find("BonusInfo").GetComponent<BonusInfo>();
        bonusTimer = GameObject.Find("BonusTimer").GetComponent<BonusTimer>();
    }

    bool DoesBonusUseTimer(int index)
    {
        foreach(int i in bonusIndexesNotUsingTimer)
        {
            if (i == index)
                return false;
        }
        return true;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            GameObject.Find("SoundManager").GetComponent<SoundManager>().Play("Bonus");

            int randomIndex = Random.Range(0, bonusTitles.Length);
            activeBonusName = bonusTitles[randomIndex];

            string bonusDescription = bonusDescriptions[randomIndex];

            bonusInfo.Display(bonusDescription);

            if (DoesBonusUseTimer(randomIndex))
                bonusTimer.StartCounting();

            Destroy(gameObject);
        }
    }
}