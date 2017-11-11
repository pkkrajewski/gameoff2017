using UnityEngine;

public class EnemyFollowPlayer : MonoBehaviour
{
    public float keepDistance = 0;

    public static float MoveSpeed;
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

            transform.rotation = Quaternion.FromToRotation(Vector3.up, direction);
            body.MovePosition((Vector2)transform.position + (Vector2)direction * MoveSpeed * Time.deltaTime);
        }
    }
}
