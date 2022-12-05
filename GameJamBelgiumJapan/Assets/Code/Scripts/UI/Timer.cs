using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timer_text;
    public float time_;
    public static float time;

    void Awake()
    {
        GameManager.gameManager.timer = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        time = time_;
    }

    // Update is called once per frame
    void Update()
    {
        timer_text.text = $"{(int)time / 60:00}:{(int)time % 60:00}";
        time -= Time.deltaTime;

        if (time <= 0)
        {
            time = 0;
            if((int)GameManager.gameManager.earthHealth.state > 1)
            {
                MenuManager.menuManager.LooseGame();
            }
            else
            {
                MenuManager.menuManager.WinGame();
            }
        }
    }

    public void Initialization()
    {
        time = time_;
    }
}
