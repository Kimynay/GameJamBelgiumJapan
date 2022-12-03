using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    //Generate the grid game
    public enum TrashType
    {
        NORMAL,
        COUNT,
    };

    [System.Serializable]
    public struct TrashPrefab
    {
        public TrashType type;
        public GameObject prefab;
    };

    public int xDim;
    public int yDim;

    public TrashPrefab[] trashPrefabs;
    public GameObject backgroundPrefab;

    private Dictionary<TrashType, GameObject> trashPrefabDict;

    private GameTrash[,] trashes;

    void Start()
    {

        trashPrefabDict = new Dictionary<TrashType, GameObject>();
        for (int i = 0; i < trashPrefabs.Length; i++)
        {
            if (!trashPrefabDict.ContainsKey(trashPrefabs[i].type))
            {
                trashPrefabDict.Add(trashPrefabs[i].type, trashPrefabs[i].prefab);
            }
        }

        for (int x = 0; x < xDim; x++)
        {
            for (int y = 0; y < yDim; y++)
            {
                float offset = 0;
                if (y % 2 != 0)
                {
                    offset = 0.5f;
                }

                GameObject background = Instantiate(backgroundPrefab, GetWorldPosition(x + offset, y * 0.866f), Quaternion.identity);
                background.transform.parent = transform;
            }
        }

        trashes = new GameTrash[xDim, yDim];
        for (int x = 0; x < xDim; x++)
        {
            for (int y = 0; y < yDim; y++)
            {
                float offset = 0;
                if (y % 2 != 0)
                {
                    offset = 0.5f;
                }
                GameObject newTrash = Instantiate(trashPrefabDict[TrashType.NORMAL], Vector3.zero, Quaternion.identity);
                newTrash.name = "Trash(" + x + "," + y + ")";
                newTrash.transform.parent = transform;
                trashes[x, y] = newTrash.GetComponent<GameTrash>();
                trashes[x, y].Init(x, y, this, TrashType.NORMAL);

                if(trashes[x, y].IsMovable())
                {
                    trashes[x, y].MovableComponent.Move(x + offset, y * 0.866f);
                }
                if (trashes[x, y].IsElemental())
                {
                    trashes[x, y].ElementalComponent.SetElement((ElementalTrash.ElementalType)UnityEngine.Random.Range(0, trashes[x, y].ElementalComponent.NumElements));
                }
            }
        }
    }

    void Update()
    {
        
    }

    public Vector2 GetWorldPosition(float x, float y)
    {
        //still have to try to change the Y position to place the grid in the center of the screen
        return new Vector2(transform.position.x - xDim/2.0f + x, transform.position.y + yDim/3.0f - y);
    }
}
