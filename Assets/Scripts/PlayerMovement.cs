using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float movementSpeed = 10;
    private float rotationSpeed = 5;
    private Rigidbody2D body;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        transform.rotation = Quaternion.FromToRotation(Vector3.up, GetDirectionToMouse());
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        int upOrDown = 0; //-1 or 1
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            upOrDown = 1;
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            upOrDown = -1;

        body.MovePosition((Vector2)transform.position + GetDirectionToMouse() * movementSpeed * upOrDown * Time.deltaTime);
    }

    private Vector2 GetDirectionToMouse()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePosition - (Vector2)transform.position;

        direction.Normalize();

        return direction;
    }
}
