using UnityEngine;

public class EnemyShootPlayer : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawn;

    public static float BulletSpeed = 5.0f;

    const float interval = 1.0f;
    float timeSinceLastShot;

    private Transform player;

    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	void Update ()
    {
        if (player == null) return;

        timeSinceLastShot += Time.deltaTime;

        if(timeSinceLastShot >= interval)
        {
            Shoot();
            timeSinceLastShot = 0;
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