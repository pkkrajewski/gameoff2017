using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField]
    public float bulletLifeTime;
    [SerializeField]
    public int bounce;

    private float triggerWait;
    private Rigidbody2D body;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>(); 
    }

    private void Update()
    {
        if (triggerWait > 0) triggerWait -= Time.deltaTime;

        bulletLifeTime -= Time.deltaTime;
        if (bulletLifeTime <= 0) Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            if (triggerWait <= 0)
                triggerWait = 3;
            else
                return;

            if (bounce > 0)
            {
                bounce--;

                Vector2 vel = body.velocity;
                vel *= -1;
                vel = vel / 2;
                if (Random.value > .5f)
                    vel.x = vel.x / 2;
                else
                    vel.y = vel.y / 2;

                body.velocity = vel;

                bulletLifeTime /= 3;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
