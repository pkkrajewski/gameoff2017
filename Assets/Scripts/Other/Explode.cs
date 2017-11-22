using UnityEngine;

public class Explode : MonoBehaviour
{
    public string[] objectsCausingExplosion;
    public float timeBeforeExplosion;
    public float radius;

    public GameObject explosionEffectPrefab;

    static string[] hittableObjectTags = new string[] {"Player", "ShootingEnemy", "ZombieEnemy", "Barrel"};

    private Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void Execute()
    {
        if (animator != null)
            if (gameObject.CompareTag("ZombieEnemy"))
                animator.Play("Spider_explode");
            else if (gameObject.CompareTag("Barrel"))
                animator.Play("Barrel_explode");

        Invoke("SendMessageToNearObjects", timeBeforeExplosion);
    }

    private void SendMessageToNearObjects()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);
        foreach (Collider2D c in colliders)
        {
            if (IsHittable(c.tag))
            {
                c.gameObject.SendMessage("OnExplosionHit");
            }
        }

        ExplosionEffect effectInstance = Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity).GetComponent<ExplosionEffect>();
        effectInstance.radius = radius;
        effectInstance.Execute();

        if (gameObject.CompareTag("ZombieEnemy"))
        {
            gameObject.GetComponent<EnemyController>().Dead();
        }
        else
            Destroy(gameObject);
    }

    bool IsHittable(string tag)
    {
        foreach(string t in hittableObjectTags)
        {
            if (t == tag)
                return true;
        }
        return false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(CanCauseExplosion(collision.gameObject.tag))
            Execute();
        DestroyIfBullet(collision.gameObject);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (CanCauseExplosion(collider.tag))
            Execute();
        DestroyIfBullet(collider.gameObject);
    }

    bool CanCauseExplosion(string tag)
    {
        foreach (string o in objectsCausingExplosion)
        {
            if (o == tag)
                return true;
        }
        return false;
    }

    void DestroyIfBullet(GameObject o)
    {
        if (o.tag == "Bullet" || o.tag == "PlayerBullet")
            Destroy(o);
    }
}