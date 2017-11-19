using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int health;
    public Sprite[] deadSprites;

    public GameObject bonusPrefab;

    [HideInInspector]
    public Room room;
    [HideInInspector]
    public SpriteRenderer spriteRenderer;

    private SoundManager soundManager;
    private SpriteFlash spriteFlash;

    public void RemoveHealth(int number)
    {
        spriteFlash.Flash(.5f, .2f);

        health -= number;

        if (health <= 0)
            Dead();

        soundManager.Play("EnemyHit");
    }

    public void TryToDropBonus(int chance)
    {
        if (Random.Range(0, chance) == 0)
            Instantiate(bonusPrefab, transform.position, Quaternion.identity);
    }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        soundManager = FindObjectOfType<SoundManager>().GetComponent<SoundManager>();
        spriteFlash = GetComponentInChildren<SpriteFlash>();
    }

    private void Start()
    {
        if (room == null)
            room = FindObjectOfType<Room>();

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

    private void Dead()
    {
        if (deadSprites.Length > 0)
        {
            GameObject dead = new GameObject("Dead Sprite " + transform.name);
            SpriteRenderer spr = dead.AddComponent<SpriteRenderer>();
            spr.sprite = deadSprites[Random.Range(0, deadSprites.Length)];
            dead.transform.position = transform.position;
            dead.transform.localScale = transform.localScale * 3.3f;
            dead.transform.SetParent(room.transform);
            dead.transform.rotation = Quaternion.Euler(0,0, Random.Range(0, 360));
        }

        TryToDropBonus(10);

        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        room.amountOfEnemies--;
        GameManager.enemiesDestroyed++;
    }
}
