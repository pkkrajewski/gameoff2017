using UnityEngine;

public class ExplodeOnExplosionHit : MonoBehaviour
{
    public void OnExplosionHit()
    {
        GetComponent<Explode>().Execute();
    }
}
