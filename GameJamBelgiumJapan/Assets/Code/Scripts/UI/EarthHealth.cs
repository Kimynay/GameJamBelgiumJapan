using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthHealth : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite[] sprites;

    public enum State
    {
        Good,
        Bad,
        Worst,
    }

    public State state = State.Bad;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Worst:
                spriteRenderer.sprite = sprites[0];
                break;

            case State.Bad:
                spriteRenderer.sprite = sprites[1];
                break;
                
            case State.Good:
                spriteRenderer.sprite = sprites[2];
                break;
        }

        if (BarsBalanceSystem.condition_value <= 50) state = State.Good;
        else if (BarsBalanceSystem.condition_value <= 100) state = State.Bad;
        else state = State.Worst;
    }
}
