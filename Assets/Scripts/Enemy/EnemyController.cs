using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int health;
    public Sprite[] deadSprites;

    [HideInInspector]
    public Room room;
    [HideInInspector]
    public SpriteRenderer spriteRenderer;

    private SoundManager soundManager;
    private SpriteFlash spriteFlash;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        soundManager = FindObjectOfType<SoundManager>().GetComponent<SoundManager>();
        spriteFlash = GetComponentInChildren<SpriteFlash>();
    }

    private void Start()
    {
        if (room == null) room = FindObjectOfType<Room>();

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
            soundManager.Play("EnemyHit");
            Destroy(collider.gameObject);
            health--;
            spriteFlash.Flash(.5f, .2f);

            if (health == 0)
            {
                if(room != null)
                    room.amountOfEnemies--;

                //Destroy(gameObject);

                Dead();
            }
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

        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        GameManager.enemiesDestroyed++;
    }
}
