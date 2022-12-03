using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    //Generate the grid game
    public enum TrashType
    {
        EMPTY,
        NORMAL,
        COUNT,
    };

    [System.Serializable]
    public struct TrashPrefab
    {
        public TrashType type;
        public GameObject prefab;
    };

    public int xDim = 5;
    public int yDim = 5;
    public float fillTime;

    public TrashPrefab[] trashPrefabs;
    public GameObject backgroundPrefab;

    private Dictionary<TrashType, GameObject> trashPrefabDict;

    private GameTrash[,] trashes;

    private GameTrash pressedTrash;
    private GameTrash previousTrash;

    void Start()
    {

        trashPrefabs[1].prefab.transform.localScale = transform.localScale;

        trashPrefabDict = new Dictionary<TrashType, GameObject>();
        for (int i = 0; i < trashPrefabs.Length; i++)
        {
            if (!trashPrefabDict.ContainsKey(trashPrefabs[i].type))
            {
                trashPrefabDict.Add(trashPrefabs[i].type, trashPrefabs[i].prefab);
            }
        }

        /*for (int x = 0; x < xDim; x++)
        {
            for (int y = 0; y < yDim; y++)
            {
                float offset = CalculateOffset(y);

                GameObject background = Instantiate(backgroundPrefab, GetWorldPosition(x + offset, y * 0.866f), Quaternion.identity);
                background.transform.parent = transform;
            }
        }*/

        trashes = new GameTrash[xDim, yDim];
        for (int x = 0; x < xDim; x++)
        {
            for (int y = 0; y < yDim; y++)
            {
                float offset = CalculateOffset(y);
                SpawnNewTrash(x, y, TrashType.EMPTY, offset);
            }
        }
        StartCoroutine(Fill());
    }

    void Update()
    {
        
    }

    public IEnumerator Fill()
    {
        while (FillStep())
        {
            yield return new WaitForSeconds(fillTime);
        }
    }

    public bool FillStep()
    {
        bool movedTrash = false;
        for(int y = yDim-2; y >= 0; y--)
        {
            for(int x = 0; x < xDim; x++)
            {
                GameTrash trash = trashes[x, y];
                if (trash.IsMovable())
                {
                    GameTrash trashBelow = trashes[x, y + 1];
                    if(trashBelow.Type == TrashType.EMPTY)
                    {
                        Destroy(trashBelow.gameObject);
                        float offset = CalculateOffset(y);
                        trash.MovableComponent.Move((x + offset)* transform.localScale.x, ((y + 1) * 0.866f)* transform.localScale.x, fillTime);
                        trashes[x, y + 1] = trash;
                        SpawnNewTrash(x, y, TrashType.EMPTY, offset);
                        movedTrash = true;
                    }
                }
            }
        }

        for(int x = 0; x < xDim; x++)
        {
            GameTrash trashBelow = trashes[x, 0];
            if(trashBelow.Type == TrashType.EMPTY)
            {
                Destroy(trashBelow.gameObject);
                float offset = CalculateOffset(-1);
                GameObject newTrash = Instantiate(trashPrefabDict[TrashType.NORMAL], Vector3.zero, Quaternion.identity);
                newTrash.transform.parent = transform;

                trashes[x, 0] = newTrash.GetComponent<GameTrash>();
                trashes[x, 0].Init(x, -1, this, TrashType.NORMAL);
                trashes[x, 0].MovableComponent.Move((x + offset) * transform.localScale.x, (0 * 0.866f) * transform.localScale.x, fillTime);
                trashes[x, 0].ElementalComponent.SetElement((ElementalTrash.ElementalType)UnityEngine.Random.Range(0, trashes[x, 0].ElementalComponent.NumElements));
                movedTrash = true;
            }
        }

        return movedTrash;
    }

    public Vector2 GetWorldPosition(float x, float y)
    {
        //still have to try to change the Y position to place the grid in the center of the screen
        return new Vector2(transform.position.x - (xDim * transform.localScale.x) / 2.0f + x, transform.position.y + (yDim * transform.localScale.x) / 3.0f - y);
    }

    public GameTrash SpawnNewTrash(int x, int y, TrashType type, float offset)
    {
        GameObject newTrash = Instantiate(trashPrefabDict[type], GetWorldPosition(x + offset, y * 0.866f), Quaternion.identity);
        newTrash.transform.parent = transform;

        trashes[x, y] = newTrash.GetComponent<GameTrash>();
        trashes[x, y].Init(x, y, this, type);

        return trashes[x, y];
    }

    private float CalculateOffset(int y)
    {
        float offset = 0;
        if (y % 2 != 0)
        {
            offset = 0.5f;
        }

        return offset;
    }

    public bool IsAdjacent(GameTrash t1, GameTrash t2)
    {
        Debug.Log(t1 + "-------------" + t2);
        return ((t1.transform.position - t2.transform.position).magnitude <= transform.localScale.x + 0.05f);
    }

    public void SwapPieces(GameTrash t1, GameTrash t2)
    {
        if(t1.IsMovable() && t2.IsMovable())
        {
            /*trashes[(int)t1.X, (int)t1.Y] = t2;
            trashes[(int)t2.X, (int)t2.Y] = t1;*/

            float t1X = t1.X;
            float t1Y = t1.Y;

            t1.MovableComponent.Move(t2.X, t2.Y, fillTime);
            t2.MovableComponent.Move(t1X, t1Y, fillTime);
        }
    }

    public void PressTrash(GameTrash t)
    {
        Debug.Log(t.X + "::::::" + t.Y);
        if (!pressedTrash)
        {
            pressedTrash = t;
            return;
        }
        else if(IsAdjacent(pressedTrash, t))
        {
            SwapPieces(pressedTrash, t);
            pressedTrash = null;
            return;
        }
        pressedTrash = t;
    }
    public void EnterTrash(GameTrash t)
    {
        previousTrash = t;
    }

    public void ReleaseTrash()
    {
        if(IsAdjacent(pressedTrash, previousTrash))
        {

            SwapPieces(pressedTrash, previousTrash);
        }
    }
}
