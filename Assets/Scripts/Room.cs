using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public GameObject bottomDoor;
    public GameObject topDoor;

    [HideInInspector]
    public Vector2 roomSize;

    private int amountOfEasyEnemies;
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

    private void Update()
    {
        if (topDoor.activeInHierarchy)
        {
            if (player != null && (player.transform.position.y - transform.position.y) > 4f)
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
        bottomDoor.SetActive(true);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!hasEntered && collision.name == "Player")
        {
            if (player == null)
                player = collision.gameObject;       

            if (player != null && (player.transform.position.y - transform.position.y) > -4.5f)
            {
                //Debug.Log(GetInstanceID());
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
