using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    private bool hasEntered;
    private CameraController cam;

    private void Awake()
    {
        cam = FindObjectOfType<CameraController>();
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
