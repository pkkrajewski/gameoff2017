using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    private bool hasEntered;
    private Camera cam;

    private void Awake()
    {
        cam = FindObjectOfType<Camera>();
    }

    private void Update()
    {
            
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasEntered && collision.name == "Player")
        {
            hasEntered = true;
            cam.SetTarget(this.transform);
        }
    }
}
