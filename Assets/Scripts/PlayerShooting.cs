using UnityEngine;

public class PlayerShooting : MonoBehaviour {

    public GameObject bulletPrefab;
    public Transform bulletSpawn;

    public float bulletSpeed;
    public float bulletLifeTime;

	void Update ()
    {
		if(Input.GetMouseButtonUp(0))
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = GetDirectionToMouse() * bulletSpeed;
            Destroy(bullet, bulletLifeTime);
        }
	}

    private Vector2 GetDirectionToMouse()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePosition - (Vector2)transform.position;

        direction.Normalize();

        return direction;
    }
}
