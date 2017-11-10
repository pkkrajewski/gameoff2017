using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowPlayer : MonoBehaviour
{
    public float keepDistance = 0;

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
        if(player != null && enemyController.spriteRenderer.color.a >= 1 && keepDistance <= Vector2.Distance(transform.position, player.transform.position))
        {
            Vector3 direction = player.transform.position - transform.position;
            transform.rotation = Quaternion.FromToRotation(Vector3.up, direction);

            transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);
        }
    }
}
