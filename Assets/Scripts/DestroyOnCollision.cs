using UnityEngine;

public class DestroyOnCollision : MonoBehaviour {

    public string[] destroyers;

    void OnTriggerEnter2D(Collider2D coll)
    {
        foreach(string d in destroyers)
        {
            if (d == coll.tag)
                Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        foreach (string d in destroyers)
        {
            Debug.Log(coll.gameObject.tag);
            if (d == coll.gameObject.tag)
                Destroy(gameObject);
        }
    }
}
