using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTrash : MonoBehaviour
{

    private int xGridPos;
    private int yGridPos;
    private float xRealPos;
    private float yRealPos;

    public int X
    {
        get { return xGridPos; }
        set { if (IsMovable())
            {
                xGridPos = value;
            } }
    }
    public int Y
    {
        get { return yGridPos; }
        set
        {
            if (IsMovable())
            {
                yGridPos = value;
            }
        }
    }
    public float x
    {
        get { return xRealPos; }
        set
        {
            if (IsMovable())
            {
                xRealPos = value;
            }
        }
    }
    public float y
    {
        get { return yRealPos; }
        set
        {
            if (IsMovable())
            {
                yRealPos = value;
            }
        }
    }

    public Grid.TrashType type;
    public Grid.TrashType Type
    {
        get { return type; }
    }

    private Grid grid;
    public Grid GridRef
    {
        get { return grid; }
    }

    private MovableTrash movableComponent;
    public MovableTrash MovableComponent
    {
        get { return movableComponent; }
    }

    private ElementalTrash elementalComponent;
    public ElementalTrash ElementalComponent
    {
        get { return elementalComponent; }
    }

    private ClearableTrash clearableComponent;
    public ClearableTrash ClearableComponent
    {
        get { return clearableComponent; }
    }

    void Awake()
    {
        movableComponent = GetComponent<MovableTrash>();
        elementalComponent = GetComponent<ElementalTrash>();
        //Debug.Log("Clear ?" + gameObject + Time.time);
        clearableComponent = GetComponent<ClearableTrash>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Init(int _X, int _Y, float _x, float _y, Grid _grid, Grid.TrashType _type)
    {
        xGridPos = _X;
        yGridPos = _Y;
        xRealPos = _x;
        yRealPos = _y;
        grid = _grid;
        type = _type;
    }

    private void OnMouseDown()
    {
        grid.PressTrash(this);
    }

    public bool IsMovable()
    {
        return movableComponent != null;
    }

    public bool IsElemental()
    {
        return elementalComponent != null;
    }

    public bool IsClearable()
    {
        return clearableComponent != null;
    }
}
