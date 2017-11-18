using UnityEngine;

public class PlayerExplosionHit : MonoBehaviour {
    
    public void OnExplosionHit()
    {
        GetComponent<PlayerHealth>().RemoveHealthPacks(2);
    }
}
