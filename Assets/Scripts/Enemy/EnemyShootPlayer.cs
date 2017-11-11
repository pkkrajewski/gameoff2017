using UnityEngine;

public class EnemyShootPlayer : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawn;

    public static float BulletSpeed;

    const float MinInterval = 0.05f;
    public static float MaxInterval;

    float currentInterval;
    float timeSinceLastShot;

    private Transform player;

    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	void Update ()
    {
        timeSinceLastShot += Time.deltaTime;

        if (timeSinceLastShot >= currentInterval)
        {
            if (player != null)
            {
                Shoot();
                timeSinceLastShot = 0;
                currentInterval = Random.Range(MinInterval, MaxInterval);
            }
        }
	}

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = GetDirectionToPlayer() * BulletSpeed;
    }

    Vector2 GetDirectionToPlayer()
    {
        Vector3 direction = player.position - transform.position;

        direction.Normalize();

        return direction;
    }
}