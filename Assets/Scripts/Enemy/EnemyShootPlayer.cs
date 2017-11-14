using UnityEngine;

public class EnemyShootPlayer : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawn;

    public static float BulletSpeed;

    const float MinInterval = 0.05f;
    public static float MaxInterval;

    float currentInterval;
    float timeSinceLastShot = -1;

    private Transform player;

    public Animator mainAnimator;
    public Animator muzzleAnimator;

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
                muzzleAnimator.Play("Muzzle");
            }
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = GetDirectionToPlayer() * BulletSpeed;
        FindObjectOfType<SoundManager>().Play("EnemyShot");
    }

    Vector2 GetDirectionToPlayer()
    {
        Vector3 direction = player.position - transform.position;

        direction.Normalize();

        return direction;
    }
}