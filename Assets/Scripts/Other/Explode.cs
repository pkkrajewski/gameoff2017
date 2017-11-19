using UnityEngine;

public class Explode : MonoBehaviour
{
    public string[] objectsCausingExplosion;
    public float timeBeforeExplosion;
    public float radius;

    public GameObject explosionEffectPrefab;

    static string[] hittableObjectTags = new string[] {"Player", "ShootingEnemy", "ZombieEnemy", "Barrel"};

    public void Execute()
    {
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

        if (gameObject.tag == "ZombieEnemy")
            gameObject.GetComponent<EnemyController>().TryToDropBonus(10);

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