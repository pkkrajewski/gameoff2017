using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public Transform healthPacksContainer;
    public GameObject healthPackPrefab;

    public SoundManager soundManager;

    public int healthPacksNumber;

    private SpriteFlash spriteFlash;

    public void AddHealthPack()
    {
        if (healthPackPrefab != null)
        {
            Instantiate(healthPackPrefab, healthPacksContainer);
        }
    }

    public void RemoveHealthPacks(int number)
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

    private void Awake()
    {
        spriteFlash = GetComponentInChildren<SpriteFlash>();
    }

    void Start ()
    {
        for (int i = 0; i < healthPacksNumber; i++)
            AddHealthPack();
	}

    void Update()
    {
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