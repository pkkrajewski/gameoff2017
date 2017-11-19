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

        if (Bonus.activeBonusName == "SlowerPlayerWalking")
            targetVelocity *= 0.75f;
        else if (Bonus.activeBonusName == "FasterPlayerWalking")
            targetVelocity *= 1.5f;

        body.velocity = targetVelocity * movementSpeed;

        if (body.velocity != Vector2.zero)
            mainAnimator.SetBool("walking", true);
        else
            mainAnimator.SetBool("walking", false);
    }
}