using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableTrash : MonoBehaviour
{
    //Enable you to move trashes
    private GameTrash trash;

    void Awake()
    {
        trash = GetComponent<GameTrash>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Move(float newX, float newY)
    {
        trash.X = newX;
        trash.Y = newY;

        trash.transform.localPosition = trash.GridRef.GetWorldPosition(newX, newY);
    }
}
