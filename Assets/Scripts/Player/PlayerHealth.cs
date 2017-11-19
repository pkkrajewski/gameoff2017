using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public Transform healthPacksContainer;
    public GameObject healthPackPrefab;

    public SoundManager soundManager;

    public int startingHealthPacksNumber;

    private int healthPacksNumber;

    private SpriteFlash spriteFlash;

    public void AddHealthPacks(int number)
    {
        for(int i = 0; i < number; i++)
            Instantiate(healthPackPrefab, healthPacksContainer);
        healthPacksNumber += number;
    }

    public void RemoveHealthPacks(int number)
    {
        if (Bonus.activeBonusName != "PlayerImmortality")
        {
            spriteFlash.Flash(.5f, .2f);

            if (number > healthPacksNumber)
                number = healthPacksNumber;

            GameObject[] healthPackSprites = GameObject.FindGameObjectsWithTag("HealthPack");
            for (int i = 0; i < number; i++)
            {
                Destroy(healthPackSprites[i]);
            }

            healthPacksNumber -= number;
        }
    }

    private void Awake()
    {
        spriteFlash = GetComponentInChildren<SpriteFlash>();
    }

    void Start ()
    {
        AddHealthPacks(startingHealthPacksNumber);
	}

    void Update()
    {
        if (Bonus.activeBonusName == "HealthPacks+1")
        {
            AddHealthPacks(1);
            Bonus.activeBonusName = "";
        }
        else if(Bonus.activeBonusName == "HealthPacks-1")
        {
            RemoveHealthPacks(1);
            Bonus.activeBonusName = "";
        }

        if (IsDead())
        {
            soundManager.Play("GameOver");
            FindObjectOfType<GameManager>().GameOver();
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D coll) {
        if (coll.gameObject.tag == "EnemyBullet")
        {
            soundManager.Play("PlayerHit");
            RemoveHealthPacks(1);
            Destroy(coll.gameObject);
        }
    }

    private bool IsDead()
    {
        return healthPacksNumber <= 0;
    }
}