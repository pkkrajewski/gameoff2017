using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Room : MonoBehaviour
{
    public GameObject bottomDoor;
    public GameObject topDoor;

    [HideInInspector]
    public Vector2 roomSize;
    [HideInInspector]
    public int amountOfEnemies;

    public static int startingAmountOfEnemies;

    private int numberOfPassedRoomsToAddEnemy = 2;
    private bool hasEntered;
    private CameraController cam;
    private GameObject player;
    private RoomManager roomManager;
    private Tilemap tilemap;
    private static List<Vector3Int> gridPositions;
    private List<Vector3Int> freeGridPositions;

    private void Awake()
    {
        cam = FindObjectOfType<CameraController>();
        roomSize = GetCurrentRoomSize();
        roomManager = FindObjectOfType<RoomManager>();
        tilemap = GetComponentInChildren<Tilemap>();
    }
        
    private void Start()
    {
        if (roomManager.roomNumber % numberOfPassedRoomsToAddEnemy == 0)
            startingAmountOfEnemies++;
        amountOfEnemies = startingAmountOfEnemies;
        InitGrid();
    }

    private void Update()
    {
        if (topDoor.activeInHierarchy)
        {
            if (player != null && (player.transform.position.y - transform.position.y) > 4f && transform.position == Vector3.zero)
                OpenTopDoor();

            if (amountOfEnemies <= 0 && transform.position != Vector3.zero)
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
        for (int i = 0; i < amountOfEnemies; i++)
        {
            GameObject newEnemy;

            if (Random.Range(0, 2) == 0)
            {
                newEnemy = Instantiate(roomManager.easyEnemyPrefab, transform);
                newEnemy.name = roomManager.easyEnemyPrefab.name;
            }
            else
            {
                newEnemy = Instantiate(roomManager.shootingEnemyPrefab, transform);
                newEnemy.name = roomManager.shootingEnemyPrefab.name;
            }

            //float offset = 1f;
            //float roomX = roomSize.x / 2 - offset;
            //float roomY = roomSize.y / 2 - offset;

            //Vector3 pos = new Vector3(Random.Range(-roomX, roomX), Random.Range(-roomY, roomY), 0);

            //if (pos.y < 3) pos.y += 2;

            //newEnemy.transform.position = transform.position + pos;

            newEnemy.transform.position = GiveFreeGridPosition();

            newEnemy.GetComponent<EnemyController>().room = this;
        }
    }

    private Vector3 GiveFreeGridPosition()
    {
        Vector3Int pos = freeGridPositions[Random.Range(0, freeGridPositions.Count)];

        freeGridPositions.Remove(new Vector3Int (pos.x -1, pos.y, 0));
        freeGridPositions.Remove(new Vector3Int(pos.x + 1, pos.y, 0));
        freeGridPositions.Remove(new Vector3Int(pos.x, pos.y - 1, 0));
        freeGridPositions.Remove(new Vector3Int(pos.x, pos.y + 1, 0));

        freeGridPositions.Remove(pos);

        return tilemap.CellToWorld(pos);
    }

    private void InitGrid()
    {
        if (tilemap != null && gridPositions == null)
        {
            gridPositions = new List<Vector3Int>();

            int offsetX = 3;
            int offsetY = 1;

            int boundsX = (int)tilemap.localBounds.max.x;
            int boundsY = (int)tilemap.localBounds.max.y;

            for (int x = -boundsX + offsetX; x < boundsX - offsetX + 1; x++)
            {
                for (int y = -boundsY + offsetY; y < boundsY - offsetY + 1; y++)
                {
                    //not letting an enemy spawn where the player can enter the room
                    if (x > -5 && x < 5 && y < -3) continue;

                    gridPositions.Add(new Vector3Int(x, y, 0));
                }
            }
        }
        else
        {
            freeGridPositions = new List<Vector3Int>(gridPositions);
        }
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
