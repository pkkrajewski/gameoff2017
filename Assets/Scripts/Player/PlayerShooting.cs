using UnityEngine;

public class PlayerShooting : MonoBehaviour {

    public GameObject bulletPrefab;
    public Transform bulletSpawn;

    public float fireRate;
    private float fireRateCounter;

    public float bulletSpeed;
    public float bulletLifeTime;
    public int bulletBounce;

    public Animator mainAnimator;
    public Animator muzzleAnimator;

    private Transform bulletsContainer;

    private SoundManager soundManager;

    void Awake()
    {
        bulletsContainer = GameObject.FindGameObjectWithTag("BulletsContainer").transform;
        soundManager = FindObjectOfType<SoundManager>().GetComponent<SoundManager>();
    }

    void Update ()
    {
        if (fireRateCounter <= 0)
        {
            if (Input.GetMouseButton(0))
            {
                soundManager.Play("PlayerShot");

                fireRateCounter = fireRate;

                if (Bonus.activeBonusName == "SlowerPlayerShooting")
                    fireRateCounter *= 1.5f;
                else if (Bonus.activeBonusName == "FasterPlayerShooting")
                    fireRateCounter *= 0.75f;

                GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity, bulletsContainer);
                bullet.GetComponent<Rigidbody2D>().velocity = GetDirectionToMouse() * bulletSpeed;
                bullet.GetComponent<BulletVelocitySaver>().Save();
                bullet.GetComponent<BulletController>().bulletLifeTime = bulletLifeTime;
                bullet.GetComponent<BulletController>().bounce = bulletBounce;

                muzzleAnimator.Play("Muzzle");
            }

        }
        else
        {
            fireRateCounter -= Time.deltaTime;
        }

        MainSpriteAngle();
    }

    private Vector2 GetDirectionToMouse()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePosition - (Vector2)transform.position;

        direction.Normalize();

        return direction;
    }

    private void MainSpriteAngle()
    {
        Vector2 dir = GetDirectionToMouse();
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        mainAnimator.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
    }
}
