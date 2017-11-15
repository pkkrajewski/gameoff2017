using UnityEngine;

public class GunsightPlacement : MonoBehaviour
{
    Vector2 cursorOffset = new Vector2(-0.22f, 0);

    void Awake()
    {
        Cursor.visible = false;
    }

	void Update ()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition += cursorOffset;

        transform.localPosition = mousePosition;
    }
}
