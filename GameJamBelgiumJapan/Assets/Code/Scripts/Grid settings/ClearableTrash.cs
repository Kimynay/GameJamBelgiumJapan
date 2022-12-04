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
        Destroy(gameObject);
        //StartCoroutine(ClearCoroutine());
    }

    /*private IEnumerator ClearCoroutine()
    {
        Destroy(gameObject);
    }*/
}
