using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EarthHealth : MonoBehaviour
{
    public Image image;
    public Sprite[] sprites;

    public enum State
    {
        Singing,
        Good,
        Interested,
        Concerned,
        Bad,
        Low,
        Critical,
    }

    public State state = State.Bad;

    void Awake()
    {
        GameManager.gameManager.earthHealth = this;
    }
    void Start()
    {
        
    }
    void Update()
    {
        switch (state)
        {
            case State.Singing:
                image.sprite = sprites[6];
                break;
            case State.Good:
                image.sprite = sprites[5];
                break;
            case State.Interested:
                image.sprite = sprites[4];
                break;
            case State.Concerned:
                image.sprite = sprites[3];
                break;
            case State.Bad:
                image.sprite = sprites[2];
                break;
            case State.Low:
                image.sprite = sprites[1];
                break;
            case State.Critical:
                image.sprite = sprites[0];
                break;
            default:
                break;
        }

        if (BarsBalanceSystem.condition_value <= (100f / 7f)) state = State.Singing;
        else if (BarsBalanceSystem.condition_value <= (100f / 7f) * 2f) state = State.Good;
        else if (BarsBalanceSystem.condition_value <= (100f / 7f) * 3f) state = State.Interested;
        else if (BarsBalanceSystem.condition_value <= (100f / 7f) * 4f) state = State.Concerned;
        else if (BarsBalanceSystem.condition_value <= (100f / 7f) * 5f) state = State.Bad;
        else if (BarsBalanceSystem.condition_value <= (100f / 7f) * 6f) state = State.Low;
        else state = State.Critical;
    }
}
