using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public GameObject bottomDoor;
    public GameObject topDoor;

    [HideInInspector]
    public Vector2 roomSize;
    [HideInInspector]
    public int amountOfEasyEnemies;

    private bool hasEntered;
    private CameraController cam;
    private GameObject player;
    private RoomManager roomManager;

    private void Awake()
    {
        cam = FindObjectOfType<CameraController>();
        roomSize = GetCurrentRoomSize();
        roomManager = FindObjectOfType<RoomManager>();
    }
        
    private void Start()
    {
        amountOfEasyEnemies = 3;
    }

    private void Update()
    {
        if (topDoor.activeInHierarchy)
        {
            if (player != null && (player.transform.position.y - transform.position.y) > 4f && transform.position == Vector3.zero)
                OpenTopDoor();

            if (amountOfEasyEnemies <= 0 && transform.position != Vector3.zero)
            {
                OpenTopDoor();
            }
        }
    }

    private Vector2 GetCurrentRoomSize()
    {
        Vector2 size = new Vector2();
        size.x = GetComponent<Collider2D>().bounds.size.x;
        size.y = GetComponent<Collider2D>().bounds.size.y;

        return size;
    }

    private void OpenTopDoor()
    {
        topDoor.SetActive(false);
    }

    private void CloseBottomDoor()
    {
        if (!bottomDoor.activeInHierarchy)
        {
            bottomDoor.SetActive(true);

            if(transform.position != Vector3.zero)
                PlaceEnemies();
        }
    }

    private void PlaceEnemies()
    {
        int easyEnemies = Random.Range(1, amountOfEasyEnemies + 1);
        for (int i = 0; i < easyEnemies; i++)
        {
            GameObject newEnemy = Instantiate(roomManager.easyEnemyPrefab, transform);
            float offset = 1f;
            float roomX = roomSize.x / 2 - offset;
            float roomY = roomSize.y / 2 - offset;

            Vector3 pos = new Vector3(Random.Range(-roomX, roomX), Random.Range(-roomY, roomY), 0);

            if (pos.y < 3) pos.y += 2;

            newEnemy.transform.position = transform.position + pos;

            newEnemy.GetComponent<EnemyController>().room = this;
        }
        amountOfEasyEnemies = easyEnemies;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            if (player == null)
                player = collision.gameObject;       

            if (!hasEntered && player != null && (player.transform.position.y - transform.position.y) > -4.5f)
            {
                roomManager.CreateNextRoom(this);

                hasEntered = true;
                cam.SetTarget(this.transform);

                CloseBottomDoor();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (hasEntered && collision.name == "Player")
        {
            Destroy(gameObject, 4);
        }
    }
}
