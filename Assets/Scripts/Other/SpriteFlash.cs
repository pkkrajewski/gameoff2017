using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFlash : MonoBehaviour
{
    public Material flashMaterial;

    private float flashTime;
    private float flashTimeBetween;
    private Material startMaterial;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start ()
    {
        startMaterial = spriteRenderer.material;
    }

    private void Update()
    {
        flashTime -= Time.deltaTime;

        spriteRenderer.material = (flashTime % flashTimeBetween >= flashTimeBetween / 2) ? flashMaterial : startMaterial;
    }

    public void Flash(float flashTime, float flashTimeBetween)
    {
        if (this.flashTime <= 0)
        {
            this.flashTime = flashTime;
            this.flashTimeBetween = flashTimeBetween;
            spriteRenderer.material = flashMaterial;
        }
    }
}
