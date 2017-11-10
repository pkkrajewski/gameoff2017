using UnityEngine;

public class GunsightPlacement : MonoBehaviour
{
    void Awake()
    {
        Cursor.visible = false;
    }

	void Update ()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.localPosition = mousePosition;
	}
}
