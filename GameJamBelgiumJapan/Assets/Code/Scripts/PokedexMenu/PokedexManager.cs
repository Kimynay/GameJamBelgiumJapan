using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PokedexManager : MonoBehaviour
{
    public Image image;
    public TextMeshProUGUI name_text;
    public TextMeshProUGUI description_text;

    [System.Serializable]
    public struct Creatures
    {
        public string name;
        public Sprite sprite;
        [TextArea(1, 10)]
        public string description;
        public bool collected;
    }

    public Creatures[] creatures;

    public RectTransform contentRectTransform;
    public GameObject buttonCell;
    public Sprite spriteCell;

    public int choice;

    // Start is called before the first frame update
    void Start()
    {
        InstantiateGridLayoutCell();
    }

    // Update is called once per frame
    void Update()
    {
        if (creatures[choice].collected)
        {
            image.sprite = creatures[choice].sprite;
            name_text.text = creatures[choice].name;
            description_text.text = creatures[choice].description;
        }
    }

    public void InstantiateGridLayoutCell()
    {
        for (int i = 0; i < creatures.Length; i++)
        {
            GameObject obj = Instantiate(buttonCell, contentRectTransform);
            if (creatures[i].collected)
            {
                obj.GetComponent<Image>().sprite = creatures[i].sprite;
                obj.GetComponent<Button>().onClick.AddListener(() => choice = obj.transform.GetSiblingIndex());
            }
            else obj.GetComponent<Image>().sprite = spriteCell;
        }
    }

    public void Back()
    {
        Debug.Log("Back!");
    }
}
