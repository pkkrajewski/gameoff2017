using UnityEngine;

public class PauseMenuState : MonoBehaviour
{
    public GameObject playState;

	void Update ()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            playState.SetActive(true);
            gameObject.SetActive(false);
        }
	}

    public void OnClickExit()
    {
        Application.Quit();
    }
}