using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterEmotions : MonoBehaviour
{
    public float waitNormal = 2.0f;
    public Image image;
    public Sprite[] sprites;
    private int currentSprite = 0;
    private bool startWaitNormal = false;
    private bool startThinking = false;
    private bool startSadThinking = false;
    private bool startHappy = false;
    private bool startVeryHappy = false;

    public enum State
    {
        Normal,
        Thinking,
        SadThinking,
        Happy,
        VeryHappy,
    }

    public State state = State.Normal;

    void Awake()
    {
        GameManager.gameManager.characterEmotions = this;
    }
    void Start()
    {

    }
    void Update()
    {
        switch (state)
        {
            case State.Normal:
                if (!startWaitNormal)
                {
                    image.sprite = sprites[0];
                    startWaitNormal = true;
                    StartCoroutine(WaitNormal());
                }
                break;
            case State.Thinking:
                if(!startThinking)
                {
                    startThinking = true;
                    StartCoroutine(Thinking());
                }
                break;
            case State.SadThinking:
                if (!startSadThinking)
                {
                    startSadThinking = true;
                    StartCoroutine(SadThinking());
                }
                break;
            case State.Happy:
                image.sprite = sprites[9];
                break;
            case State.VeryHappy:
                if (!startVeryHappy)
                {
                    image.sprite = sprites[10];
                    startVeryHappy = true;
                    StartCoroutine(VeryHappy());
                }
                break;
            default:
                break;
        }
        if((int)GameManager.gameManager.earthHealth.state == 0)
        {
            state = State.Happy;
        }
        else if(state == State.Happy)
        {
            state = State.Normal;
        }
    }
    public IEnumerator WaitNormal()
    {
        yield return new WaitForSeconds(waitNormal);
        if((int)GameManager.gameManager.earthHealth.state > 3)
        {
            state = State.SadThinking;
            image.sprite = sprites[5];
            currentSprite = 5;
        }
        else
        {
            state = State.Thinking;
            image.sprite = sprites[1];
            currentSprite = 1;
        }
        startWaitNormal = false;
    }

    public IEnumerator Thinking()
    {
        while(currentSprite < 4)
        {
            yield return new WaitForSeconds(waitNormal);
            image.sprite = sprites[currentSprite + 1];
            currentSprite++;
            Debug.Log("think");
        }
        state = State.Normal;
        startThinking = false;
    }

    public IEnumerator SadThinking()
    {
        while (currentSprite < 8)
        {
            yield return new WaitForSeconds(waitNormal);
            image.sprite = sprites[currentSprite + 1];
            currentSprite++;
            Debug.Log("think");
        }
        state = State.Normal;
        startSadThinking = false;
    }
    public void SetVeryHappy()
    {
        state = State.VeryHappy;
    }
    //public IEnumerator Happy()
    //{
    //    yield return new WaitForSeconds(waitNormal);
    //
    //}
    //
    public IEnumerator VeryHappy()
    {
        yield return new WaitForSeconds(waitNormal);
        state = State.Normal;
        startVeryHappy = false;
    }
}
