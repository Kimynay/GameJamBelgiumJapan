 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarsBalanceSystem : MonoBehaviour
{
    public GameObject[] bar_type;

    public static float condition_value;

    public Slider[] bars;

    public float width;

    public float increasePerTile = 1.0f;

    void Awake()
    {
        GameManager.gameManager.barsBalanceSystem = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        bars = new Slider[(int)bar_type.Length];
        for (int i = 0; i < bar_type.Length; i++)
        {
            GameObject bar = Instantiate(bar_type[i]);
            bars[i] = bar.GetComponent<Slider>();
            bars[i].transform.position = new Vector3(0, (i - (float)bar_type.Length / 2 + 0.5f) * -width, transform.localPosition.z);
            bar.transform.SetParent(transform, false);
            bars[i].value = 60;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach (Slider i in bars) i.value -= Time.deltaTime * 1.0f;

        condition_value = Mathf.Abs(bars[0].value - 60) + Mathf.Abs(bars[1].value - 60) + Mathf.Abs(bars[2].value - 60);
    }

    public void Initialization()
    {
        foreach (Slider i in bars) i.value = 60;
    }

    public void IncreaseElement(int i)
    {
        StartCoroutine("AddElemental", i);
    }

    IEnumerator AddElemental(int j)
    {
        for (int i = 0; i < 5; i++)
        {
            bars[j].value += increasePerTile / 5.0f;
            yield return new WaitForSeconds(0.05f);
        }
    }
}
