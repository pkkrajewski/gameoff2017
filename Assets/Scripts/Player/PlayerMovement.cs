using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float movementSpeed = 10;
    private Rigidbody2D body;

    public Animator mainAnimator;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Vector2 targetVelocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        body.velocity = targetVelocity * movementSpeed;

        if (body.velocity != Vector2.zero)
            mainAnimator.SetBool("walking", true);
        else
            mainAnimator.SetBool("walking", false);
    }
}