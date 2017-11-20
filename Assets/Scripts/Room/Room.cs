using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Room : MonoBehaviour
{
    public GameObject bottomDoor;
    public GameObject topDoor;

    private GameObject playState;

    private SoundManager soundManager;

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
    private static List<Sprite> tilemapSprites;
    private static List<Vector3Int> gridPositions;
    private List<Vector3Int> freeGridPositions;

    private void Awake()
    {
        playState = FindObjectOfType<PlayState>().gameObject;
        soundManager = FindObjectOfType<SoundManager>();
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

        roomManager.exitHint.SetActive(false);

        soundManager.Play("RoomEntering");
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
                roomManager.exitHint.SetActive(true);
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

    private void ShuffleTilemap()
    {
        var shuffledList = tilemapSprites.OrderBy(x => Random.value).ToList();

        int boundsX = (int)tilemap.localBounds.max.x;
        int boundsY = (int)tilemap.localBounds.max.y;

        int a = 0;

        for (int x = -boundsX; x < boundsX; x++)
        {
            for (int y = -boundsY; y < boundsY; y++)
            {
                a++;
                if (a >= gridPositions.Count) break;
                Tile tile = (Tile)ScriptableObject.CreateInstance(typeof(Tile));
                tile.sprite = shuffledList[a];
                tilemap.SetTile(gridPositions[a], tile);
            }
        }
    }

    private void InitGrid()
    {
        if (tilemap != null && gridPositions == null)
        {
            gridPositions = new List<Vector3Int>();
            tilemapSprites = new List<Sprite>();

            int offsetX = 3;
            int offsetY = 1;

            int boundsX = (int)tilemap.localBounds.max.x;
            int boundsY = (int)tilemap.localBounds.max.y;

            for (int x = -boundsX + offsetX; x < boundsX - offsetX + 1; x++)
            {
                for (int y = -boundsY + offsetY; y < boundsY - offsetY + 1; y++)
                {
                    tilemapSprites.Add(tilemap.GetSprite(new Vector3Int(x, y, 0)));

                    //not letting an enemy spawn where the player can enter the room
                    if (x > -5 && x < 5 && y < -3) continue;

                    gridPositions.Add(new Vector3Int(x, y, 0));
                }
            }
        }
        else
        {
            freeGridPositions = new List<Vector3Int>(gridPositions);

            ShuffleTilemap();
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
        if (playState.activeInHierarchy)
        {
            if (hasEntered && collision.name == "Player")
            {
                Invoke("DestroyMe", 4);
            }

            if (player == null)
            {
                //so the room doesn't get destroyed when gameover
                CancelInvoke();
            }
        }
    }

    public void DestroyMe()
    {
        Destroy(gameObject);
    }
}
