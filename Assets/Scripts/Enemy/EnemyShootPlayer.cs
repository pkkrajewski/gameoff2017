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

    private Transform bulletsContainer;

    private Transform player;
    private SoundManager soundManager;

    public Animator mainAnimator;
    public Animator muzzleAnimator;

    void Start ()
    {
        bulletsContainer = GameObject.FindGameObjectWithTag("BulletsContainer").transform;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        soundManager = FindObjectOfType<SoundManager>().GetComponent<SoundManager>();
	}
	
	void Update ()
    {
        if (player != null)
        {
            timeSinceLastShot += Time.deltaTime;

            if (timeSinceLastShot >= currentInterval)
            {
                Shoot();
                timeSinceLastShot = 0;

                currentInterval = Random.Range(MinInterval, MaxInterval);
                if (Bonus.activeBonusName == "FasterEnemyShooting")
                    currentInterval *= 0.75f;
                else if (Bonus.activeBonusName == "SlowerEnemyShooting")
                    currentInterval *= 1.5f;

                muzzleAnimator.Play("Muzzle");
            }

            if (gameObject.name == "TankEnemy")
            {
                transform.GetChild(0).transform.rotation = Quaternion.FromToRotation(Vector3.up, GetDirectionToPlayer());
                currentInterval = 2;
            }
            else
                transform.rotation = Quaternion.FromToRotation(Vector3.up, GetDirectionToPlayer());
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity, bulletsContainer);
        bullet.GetComponent<Rigidbody2D>().velocity = GetDirectionToPlayer() * BulletSpeed;
        bullet.GetComponent<BulletVelocitySaver>().Save();
        soundManager.Play("EnemyShot");
    }

    Vector2 GetDirectionToPlayer()
    {
        Vector3 direction = player.position - transform.position;

        direction.Normalize();

        return direction;
    }


}