using UnityEngine;

public class EnemyFollowPlayer : MonoBehaviour
{
    public float keepDistance = 0;
    public static float MoveSpeed;
    public Animator mainAnimator;

    private GameObject player;
    private EnemyController enemyController;
    private Rigidbody2D body;

    private void Awake()
    {
        player = FindObjectOfType<PlayerMovement>().gameObject;
        enemyController = GetComponent<EnemyController>();
        body = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if(player != null && enemyController.spriteRenderer.color.a >= 1 && keepDistance <= Vector2.Distance(transform.position, player.transform.position))
        {
            Vector3 direction = player.transform.position - transform.position;
            direction.Normalize();

            float scalar = 1.0f;

            if (Bonus.activeBonusName == "FasterEnemyWalking")
                scalar = 1.5f;
            else if (Bonus.activeBonusName == "SlowerEnemyWalking")
                scalar = 0.75f;

            float spd = MoveSpeed;

            if (gameObject.name == "TankEnemy")
            {
                spd = MoveSpeed / 3;

                //tank can only move 1 direction at a time
                if (direction.x < direction.y)
                    direction.y = 0;
                else
                    direction.x = 0;

                //mainAnimator.transform.rotation = Quaternion.FromToRotation(Vector3.up, direction);
                body.MovePosition((Vector2)transform.position + (Vector2)direction * spd * scalar * Time.deltaTime);
            }
            else
            {
                body.MovePosition((Vector2)transform.position + (Vector2)direction * spd * scalar * Time.deltaTime);
            }


            if (mainAnimator)
                mainAnimator.SetBool("walking", true);

        }
        else
            if (mainAnimator)
            mainAnimator.SetBool("walking", false);
    }
}
