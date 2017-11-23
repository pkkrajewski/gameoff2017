using UnityEngine;
using UnityEngine.UI;

public class RoomManager : MonoBehaviour
{
    public GameObject roomPrefab;
    public GameObject easyEnemyPrefab;
    public GameObject shootingEnemyPrefab;
    public GameObject barrelPrefab;
    public GameObject tankPrefab;

    public GameObject playingInstructions;
    public GameObject exitHint;
    
    [HideInInspector]
    public Room currentRoom;
    [HideInInspector]
    public int roomNumber = 0;

    private Text roomText;

    private void Awake()
    {
        //CreateNextRoom(GetComponentInChildren<Room>());

        roomText = GameObject.Find("Text Room Number").GetComponent<Text>();
    }

    public void CreateNextRoom(Room currentRoom)
    {
        BeforeCreatingRoom();
        this.currentRoom = currentRoom;

        GameObject newRoom = Instantiate(roomPrefab);
        newRoom.transform.SetParent(this.transform);
        Room room = newRoom.GetComponent<Room>();
        float x = currentRoom.transform.position.x;
        float y = currentRoom.transform.position.y + room.roomSize.y;
        newRoom.transform.position = new Vector2(x, y);

        roomText.text = "" + roomNumber;
    }

    private void BeforeCreatingRoom()
    {
        roomNumber++;

        if (roomNumber > 1)
            playingInstructions.SetActive(false);

        EnemyShootPlayer.MaxInterval -= 0.04f;
        EnemyShootPlayer.BulletSpeed += 0.3f;
        EnemyFollowPlayer.MoveSpeed += 0.09f;
    }
}
