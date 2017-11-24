using UnityEngine;

public class EnemyShootPlayer : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public Transform secondBulletSpawn;

    public static float BulletSpeed;

    const float MinInterval = 0.05f;
    public static float MaxInterval;

    float currentInterval;
    float timeSinceLastShot = -1;

    private Transform instantiatedObjects;

    private Transform player;
    private SoundManager soundManager;

    public Animator mainAnimator;
    public Animator muzzleAnimator;
    public Animator secondMuzzleAnimator;

    void Start ()
    {
        instantiatedObjects = GameObject.FindGameObjectWithTag("InstantiatedObjects").transform;
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
                if(secondMuzzleAnimator != null) secondMuzzleAnimator.Play("Muzzle");
            }

            if (gameObject.name == "TankEnemy" || gameObject.name == "TurretEnemy")
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
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity, instantiatedObjects);
        bullet.GetComponent<Rigidbody2D>().velocity = GetDirectionToPlayer() * BulletSpeed;
        bullet.GetComponent<BulletVelocitySaver>().Save();
        soundManager.Play("EnemyShot");

        if(gameObject.name == "TurretEnemy" && secondBulletSpawn != null)
        {
            GameObject bullet2 = Instantiate(bulletPrefab, secondBulletSpawn.position, Quaternion.identity, instantiatedObjects);
            bullet2.GetComponent<Rigidbody2D>().velocity = bullet.GetComponent<Rigidbody2D>().velocity;
            bullet2.GetComponent<BulletVelocitySaver>().Save();

            bullet.transform.localScale = bullet.transform.localScale / 1.5f;
            bullet2.transform.localScale = bullet2.transform.localScale / 1.5f;
        }
    }

    Vector2 GetDirectionToPlayer()
    {
        Vector3 direction = player.position - transform.position;

        direction.Normalize();

        return direction;
    }


}