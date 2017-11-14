using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int health;
    [HideInInspector]
    public Room room;
    [HideInInspector]
    public SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
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
            FindObjectOfType<SoundManager>().Play("EnemyHit");
            Destroy(collider.gameObject);
            health--;

            if (health == 0)
            {
                if(room != null)
                    room.amountOfEnemies--;

                Destroy(gameObject);
            }
        }
    }

    private void OnDestroy()
    {
        GameManager.enemiesDestroyed++;
    }
}
