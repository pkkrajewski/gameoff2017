using UnityEngine;
using UnityEngine.SceneManagement;

public class StartingScreenManager : MonoBehaviour
{
	void Update ()
    {
        if (Input.anyKeyDown)
            SceneManager.LoadScene("Game");
	}
}
