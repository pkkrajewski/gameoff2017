using UnityEngine;

public class ExplosionEffect : MonoBehaviour {

    [HideInInspector]
    public float radius;

    SpriteRenderer spriteRenderer;
    float timeSinceStartExecuting;

    const float LivingTime = 0.25f;
    const float radiusScalar = 13.53f;

    private SoundManager soundManager;

    private void Awake()
    {
        soundManager = FindObjectOfType<SoundManager>().GetComponent<SoundManager>();
    }

    private void Start()
    {
        soundManager.Play("Explosion");
    }

    public void Execute()
    {
        transform.localScale = new Vector2(radius * radiusScalar, radius * radiusScalar);
        timeSinceStartExecuting = 0;
    }

    void Update()
    {
        timeSinceStartExecuting += Time.deltaTime;

        if (timeSinceStartExecuting >= LivingTime)
            Destroy(gameObject);
    }
}
