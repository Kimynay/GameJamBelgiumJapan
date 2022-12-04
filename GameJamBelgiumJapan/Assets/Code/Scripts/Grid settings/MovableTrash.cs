using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableTrash : MonoBehaviour
{
    //Enable you to move trashes
    private GameTrash trash;
    private IEnumerator moveCoroutine;

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

    public void Move(float newX, float newY, float time)
    {

        if(moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
        }
        moveCoroutine = MoveCoroutine(newX, newY, time);
        StartCoroutine(moveCoroutine);
    }

    private IEnumerator MoveCoroutine(float newX, float newY, float time)
    {

        trash.X = newX;
        trash.Y = newY;

        Vector3 startPos = transform.position;
        Vector3 endPos = trash.GridRef.GetWorldPosition(newX, newY);

        for (float t = 0; t <= 1 * time; t += Time.deltaTime)
        {
            trash.transform.position = Vector3.Lerp(startPos, endPos, t / time);
            yield return 0;
        }
        trash.transform.position = endPos;
    }
}
