using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour {

    public Transform healthPacksContainer;
    public GameObject healthPackPrefab;

    public int healthPacksNumber = 4;

	void Start ()
    {
        for (int i = 0; i < healthPacksNumber; i++)
            AddHealthPack();
	}

    void Update()
    {
        if (IsDead())
            SceneManager.LoadScene("Game");
    }

    void AddHealthPack()
    {
        if (healthPackPrefab != null)
        {
            Instantiate(healthPackPrefab, healthPacksContainer);
        }
    }

    void RemoveHealthPack()
    {
        Destroy(GameObject.FindGameObjectWithTag("HealthPack"));
        healthPacksNumber--;
    }

    public bool IsDead()
    {
        //Debug.Log(healthPacksNumber);
        return healthPacksNumber <= 0;
    }
}
