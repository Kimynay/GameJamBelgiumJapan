using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
    private Collider2D[] neighbourCells = new Collider2D[7];

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
        bool needsRefill = true;

        while (needsRefill)
        {
            yield return new WaitForSeconds(fillTime);
            while (FillStep())
            {
                yield return new WaitForSeconds(fillTime);
            }
        }
        needsRefill = ClearAllValidMatches();
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
        return ((t1.transform.position - t2.transform.position).magnitude <= transform.localScale.x + 0.05f);
    }

    public void SwapPieces(GameTrash t1, GameTrash t2)
    {
        if(t1.IsMovable() && t2.IsMovable())
        {
            //change condition (work only if already a match before moving)
            //condition if we only want to move the pieces if there is a match
            //if (GetMatch(t1) != null || GetMatch(t2) != null)
            //{
            GetMatch(t1);
            GetMatch(t2);
                
                float t1X = t1.X;
                float t1Y = t1.Y;

                t1.MovableComponent.Move(t2.X, t2.Y, fillTime);
                t2.MovableComponent.Move(t1X, t1Y, fillTime);

                //ClearAllValidMatches();

            StartCoroutine(Fill());
            //}
        }
    }

    public void PressTrash(GameTrash t)
    {
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

    public List<GameTrash> GetMatch(GameTrash t)
    {
        if (t.IsElemental())
        {
            ElementalTrash.ElementalType element = t.ElementalComponent.Element;
            List<GameTrash> matchingTrashes = new List<GameTrash>();
            Queue<GameTrash> queue = new Queue<GameTrash>();
            HashSet<GameTrash> visited = new HashSet<GameTrash>();

            queue.Enqueue(t);
            matchingTrashes.Add(t);
            while (queue.Count > 0)
            {
                t = queue.Dequeue();
                if (!visited.Contains(t))
                {
                    visited.Add(t);
                    neighbourCells = Physics2D.OverlapCircleAll(t.transform.position, transform.localScale.x + 0.05f);
                    GameTrash tr;
                    foreach (Collider2D col in neighbourCells.Where(c => c != null))
                    {
                        tr = col.GetComponent<GameTrash>();
                        if (!tr)
                            continue;
                        if (tr.IsElemental() && tr.ElementalComponent.Element == element && !visited.Contains(tr))
                        {
                            queue.Enqueue(tr);
                            matchingTrashes.Add(tr);
                        }
                    }
                }
            }  

            if(matchingTrashes.Count >= 3)
            {
                foreach (GameTrash mt in matchingTrashes)
                {
                    mt.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.red;
                }
                Debug.Log("matching ok " + matchingTrashes.Count);
                return matchingTrashes;
            }
        }
        return null;
    }

    public bool ClearAllValidMatches()
    {
        bool needsReffil = false;
        for(int y = 0; y < yDim; y++)
        {
            for(int x = 0; x < xDim; x++)
            {
                if(trashes[x, y].IsClearable())
                {
                    List<GameTrash> match = GetMatch(trashes[x, y]);
                    if(match != null)
                    {
                        foreach(GameTrash m in match)
                        {
                            float offset = CalculateOffset(y);
                            //!!!!!!!!!!!!!!!!!!!!!!!
                            if(ClearTrash((int)m.Y, (int)m.X, offset))
                            {
                                needsReffil = true;
                            }
                        }
                    }
                }
            }
        }
        return needsReffil;
    }

    public bool ClearTrash(int x, int y, float offset)
    {
        if (trashes[x, y].IsClearable() && !trashes[x, y].ClearableComponent.IsBeingCleared) 
        {
            trashes[x, y].ClearableComponent.Clear();
            SpawnNewTrash(x, y, TrashType.EMPTY, offset);
            return true;
        }
        return false;
    }

}
