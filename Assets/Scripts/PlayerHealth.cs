using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    public Transform healthPacksContainer;
    public GameObject healthPackPrefab;

    int healthPacksNumber = 4;

	void Start ()
    {
        for (int i = 0; i < healthPacksNumber; i++)
            AddHealthPack();
        Debug.Log(healthPacksNumber);
	}
	
	void Update ()
    {
		
	}

    void AddHealthPack()
    {
        Instantiate(healthPackPrefab, healthPacksContainer);
        healthPacksNumber++;
    }

    void RemoveHealthPack()
    {
        Destroy(GameObject.FindGameObjectWithTag("HealthPack"));
        healthPacksNumber--;
    }
}
