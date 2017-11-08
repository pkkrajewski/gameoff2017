using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowPlayer : MonoBehaviour
{
    private float moveSpeed = 1;
    private GameObject player;
    private EnemyController enemyController;

    private void Awake()
    {
        player = FindObjectOfType<PlayerMovement>().gameObject;
        enemyController = GetComponent<EnemyController>();
    }

    private void Update()
    {
        if (player == null)
        {
            player = FindObjectOfType<PlayerMovement>().gameObject;
        }
        else if(enemyController.spriteRenderer.color.a >= 1)
        {
            Vector3 direction = player.transform.position - transform.position;
            transform.rotation = Quaternion.FromToRotation(Vector3.up, direction);

            transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);
        }
    }

}
