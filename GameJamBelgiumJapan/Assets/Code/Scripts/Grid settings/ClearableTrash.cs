using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearableTrash : MonoBehaviour
{
    private bool isBeingCleared = false;

    public bool IsBeingCleared
    {
        get { return isBeingCleared; }
    }

    protected GameTrash trash;

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

    public void Clear()
    {
        isBeingCleared = true;
        Debug.Log((int)gameObject.GetComponent<ElementalTrash>().Element / 3);
        GameManager.gameManager.barsBalanceSystem.IncreaseElement((int)gameObject.GetComponent<ElementalTrash>().Element / 3);
        Destroy(gameObject);
    }
}
