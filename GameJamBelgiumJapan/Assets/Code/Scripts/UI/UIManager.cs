using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private bool timer_;
    public static bool timer;

    [SerializeField]
    private bool bars_balance_system_;
    public static bool bars_balance_system;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer = timer_;
        bars_balance_system = bars_balance_system_;

        if (Timer.time == 0) bars_balance_system = false;
    }
}
