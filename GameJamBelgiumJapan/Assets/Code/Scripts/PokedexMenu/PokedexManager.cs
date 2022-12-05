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
    private GameObject[] instantiatedGrid;

    public int choice;

    private void Awake()
    {
        instantiatedGrid = new GameObject[6];
        MenuManager.menuManager.pokedexManager = this;
    }
    private void OnEnable()
    {
        InstantiateGridLayoutCell();
        choice = 0;
        StartCoroutine(VeryHappy());
    }

    void Update()
    {
        //InstantiateGridLayoutCell();
        if (creatures[choice].collected)
        {
            image.sprite = creatures[choice].sprite;
            name_text.text = creatures[choice].name;
            description_text.text = creatures[choice].description;
        }
        else
        {
            image.sprite = spriteCell;
            name_text.text = "Name";
            description_text.text = "Descritpion";
        }
    }

    private void Pressed(Button button)
    {
        //choice = button.transform.GetSiblingIndex()
    }

    public void InstantiateGridLayoutCell()
    {
        for (int i = 0; i < creatures.Length; i++)
        {
            if (instantiatedGrid[i] == null)
                instantiatedGrid[i] = Instantiate(buttonCell, contentRectTransform);

            if (creatures[i].collected)
            {
                instantiatedGrid[i].GetComponent<Image>().sprite = creatures[i].sprite;
                Debug.Log(instantiatedGrid[i].transform.GetSiblingIndex());
                
                //instantiatedGrid[i].GetComponent<RectTransform>().localScale = Vector3.one;
            }
            else
            {
                Debug.Log(instantiatedGrid[i].transform.GetSiblingIndex());
                instantiatedGrid[i].GetComponent<Image>().sprite = spriteCell;
                instantiatedGrid[i].GetComponent<RectTransform>().localScale = Vector3.one * 0.5f;
            }
            //instantiatedGrid[i].GetComponent<Button>().onClick.AddListener(() => choice = transform.GetSiblingIndex());
            //instantiatedGrid[i].GetComponent<Button>().onClick.a
        }
    }

    public IEnumerator VeryHappy()
    {
        while(gameObject.activeSelf)
        {
            yield return new WaitForSeconds(5.0f);
            choice++;
            if (choice > 5)
                choice = 0;
        }
    }

    public void Back()
    {
        Debug.Log("Back!");
    }
}
