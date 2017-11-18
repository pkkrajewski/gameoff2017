using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int health;
    [HideInInspector]
    public Room room;
    [HideInInspector]
    public SpriteRenderer spriteRenderer;

    private SoundManager soundManager;

    private SpriteFlash spriteFlash;

    public void RemoveHealth(int number)
    {
        soundManager.Play("EnemyHit");
        spriteFlash.Flash(.5f, .2f);

        health -= number;

        if (health <= 0)
            Destroy(gameObject);
    }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        soundManager = FindObjectOfType<SoundManager>().GetComponent<SoundManager>();
        spriteFlash = GetComponentInChildren<SpriteFlash>();
    }

    private void Start()
    {
        Color color = spriteRenderer.color;
        color.a = 0;
        spriteRenderer.color = color;
    }

    private void Update()
    {
        FadeIn();
    }

    private void FadeIn()
    {
        if (spriteRenderer.color.a < 1)
        {
            Color color = spriteRenderer.color;
            color.a += 1 * Time.deltaTime;
            spriteRenderer.color = color;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Bullet")
        {
            Destroy(collider.gameObject);
            RemoveHealth(1);
        }
    }

    private void OnDestroy()
    {
        room.amountOfEnemies--;
        GameManager.enemiesDestroyed++;
    }
}
