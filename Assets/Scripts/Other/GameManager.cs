using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameObject hudPanel;
    private GameObject gameoverPanel;

    private void Awake()
    {
        hudPanel = GameObject.Find("hud");
        gameoverPanel = GameObject.Find("GameOver");
    }

    private void Start()
    {
        gameoverPanel.SetActive(false);
    }
}
