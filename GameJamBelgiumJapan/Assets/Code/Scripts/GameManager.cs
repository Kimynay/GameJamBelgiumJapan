using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public Timer timer;
    public BarsBalanceSystem barsBalanceSystem;
    public Grid grid;
    private void Awake()
    {
        gameManager = this;
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        
    }
}
