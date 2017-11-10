using UnityEngine;
using UnityEngine.UI;

public class RoomManager : MonoBehaviour
{
    public GameObject roomPrefab;
    public GameObject easyEnemyPrefab;
    public GameObject shootingEnemyPrefab;
    
    [HideInInspector]
    public Room currentRoom;

    private int roomNumber = 0;
    private Text roomText;

    private void Awake()
    {
        //CreateNextRoom(GetComponentInChildren<Room>());

        roomText = GameObject.Find("Text Room Number").GetComponent<Text>();
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

        roomNumber++;
        if (roomText != null) roomText.text = "room: " + roomNumber;
    }
}
