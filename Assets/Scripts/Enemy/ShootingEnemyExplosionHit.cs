using UnityEngine;

public class ShootingEnemyExplosionHit : MonoBehaviour {

	public void OnExplosionHit()
    {
        GetComponent<EnemyController>().RemoveHealth(2);
    }
}