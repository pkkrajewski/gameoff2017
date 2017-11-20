using UnityEngine;

public class BulletVelocitySaver : MonoBehaviour
{
    Vector2 velocity;
    private Rigidbody2D body;

    public void Save()
    {
        velocity = body.velocity;
    }

    void Awake ()
    {
        body = GetComponent<Rigidbody2D>();
	}

    void OnEnable()
    {
        body.velocity = velocity;
    }
}
