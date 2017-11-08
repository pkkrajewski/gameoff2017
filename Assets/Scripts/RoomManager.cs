using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public GameObject roomPrefab;
    public GameObject easyEnemyPrefab;
    
    [HideInInspector]
    public Room currentRoom;

    private void Awake()
    {
        //CreateNextRoom(GetComponentInChildren<Room>());
    }

    public void CreateNextRoom(Room currentRoom)
    {
        this.currentRoom = currentRoom;

        GameObject newRoom = Instantiate(roomPrefab);
        newRoom.transform.SetParent(this.transform);
        Room room = newRoom.GetComponent<Room>();
        float x = currentRoom.transform.position.x;
        float y = currentRoom.transform.position.y + room.roomSize.y;
        newRoom.transform.position = new Vector2(x, y);
    }
}
