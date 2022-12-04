using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementalTrash : MonoBehaviour
{
    //Select the different elements of the trashes
    public enum ElementalType
    {
        FIRE_CIGARET,
        FIRE_GASOIL,
        FIRE_GASOIL2,
        WATER_BEER,
        WATER_BOTTLE,
        WATER_BOTTLE2,
        EARTH_BANANA,
        EARTH_GARBAGE,
        EARTH_GARBAGE2,
        ANY,
        COUNT,
    }

    [System.Serializable]
    public struct ElementalSprite
    {
        public ElementalType element;
        public Sprite sprite;
    }

    public ElementalSprite[] elementalSprites;

    private ElementalType element;

    public ElementalType Element
    {
        get { return element; }
        set { SetElement(value); }
    }

    public int NumElements
    {
        get { return elementalSprites.Length; }
    }

    private SpriteRenderer sprite;
    private Dictionary<ElementalType, Sprite> elementalTypeDict;

    private Dictionary<ElementalType, Sprite> elementalSpriteDict;

    void Awake()
    {
        sprite = transform.Find("trash").GetComponent<SpriteRenderer>();
        elementalSpriteDict = new Dictionary<ElementalType, Sprite>();
        for(int i = 0; i < elementalSprites.Length; i++)
        {
            if (!elementalSpriteDict.ContainsKey(elementalSprites[i].element))
            {
                elementalSpriteDict.Add(elementalSprites[i].element, elementalSprites[i].sprite);
            }
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void SetElement(ElementalType newElement)
    {
        element = newElement;

        if (elementalSpriteDict.ContainsKey(newElement))
        {
            sprite.sprite = elementalSpriteDict[newElement];
        }
    }
}
