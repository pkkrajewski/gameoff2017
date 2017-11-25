using UnityEngine;

public class PlayerExplosionHit : MonoBehaviour {
    
    public void OnExplosionHit()
    {
        if(Bonus.activeBonusName != "ExplosionNotHurtingPlayer")
            GetComponent<PlayerHealth>().RemoveHealthPacks(2);
    }
}