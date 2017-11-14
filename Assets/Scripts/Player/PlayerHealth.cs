using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public Transform healthPacksContainer;
    public GameObject healthPackPrefab;

    public SoundManager soundManager;

    public int healthPacksNumber;

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

    void AddHealthPack()
    {
        if (healthPackPrefab != null)
        {
            Instantiate(healthPackPrefab, healthPacksContainer);
        }
    }

    void RemoveHealthPack()
    {
        Destroy(GameObject.FindGameObjectWithTag("HealthPack"));
        healthPacksNumber--;
    }

    void OnTriggerEnter2D(Collider2D coll) {
        if (coll.gameObject.tag == "EnemyBullet")
        {
            soundManager.Play("PlayerHit");
            RemoveHealthPack();
            Destroy(coll.gameObject);
        }
    }

    private bool IsDead()
    {
        return healthPacksNumber <= 0;
    }
}