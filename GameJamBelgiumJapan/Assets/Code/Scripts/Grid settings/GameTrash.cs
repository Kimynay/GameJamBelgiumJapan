using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTrash : MonoBehaviour
{

    private float x;
    private float y;

    public float X
    {
        get { return x; }
        set { if (IsMovable())
            {
                x = value;
            } }
    }
    public float Y
    {
        get { return y; }
        set
        {
            if (IsMovable())
            {
                y = value;
            }
        }
    }

    private Grid.TrashType type;
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

    void Awake()
    {
        movableComponent = GetComponent<MovableTrash>();
        elementalComponent = GetComponent<ElementalTrash>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Init(int _x, int _y, Grid _grid, Grid.TrashType _type)
    {
        x = _x;
        y = _y;
        grid = _grid;
        type = _type;
    }

    private void OnMouseEnter()
    {
        grid.EnterTrash(this);
    }

    private void OnMouseDown()
    {
        grid.PressTrash(this);
    }

    private void OnMouseUp()
    {
        grid.ReleaseTrash();
    }

    public bool IsMovable()
    {
        return movableComponent != null;
    }

    public bool IsElemental()
    {
        return elementalComponent != null;
    }
}
