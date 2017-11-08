using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float movementSpeed = 10;
    private Rigidbody2D body;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Vector2 targetVelocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        body.velocity = targetVelocity * movementSpeed;
    }
}