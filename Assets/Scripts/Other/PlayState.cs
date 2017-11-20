using UnityEngine;

public class PlayState : MonoBehaviour
{
    public GameObject pauseMenuState;
    
	void Update ()
    {
		if(Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenuState.SetActive(true);
            gameObject.SetActive(false);
        }
	}
}
