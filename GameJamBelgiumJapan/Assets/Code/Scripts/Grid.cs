using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{

    public enum TrashType
    {
        FIRE,
        WATER,
        GROUND,
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

    /*float hexWidth = 1.732f;
    float hexHeight = 2.0f;
    public float gap = 0.0f;*/

    public TrashPrefab[] trashPrefabs;
    public GameObject backgroundPrefab;

    private Dictionary<TrashType, GameObject> trashPrefabDict;
    //Vector3 startpos;

    // Start is called before the first frame update
    void Start()
    {
        /*AddGap();
        CalcStartPos();*/

        trashPrefabDict = new Dictionary<TrashType, GameObject>();
        for(int i = 0; i < trashPrefabs.Length; i++)
        {
            if (!trashPrefabDict.ContainsKey(trashPrefabs[i].type))
            {
                trashPrefabDict.Add(trashPrefabs[i].type, trashPrefabs[i].prefab);
            }
        }

        for(int x = 0; x < xDim; x++)
        {
            for(int y = 0; y < yDim; y++)
            {
                GameObject background = Instantiate(backgroundPrefab, new Vector3(x, y, 0), Quaternion.identity);
                background.transform.parent = transform;
                /*Transform hex = Instantiate(backgroundPrefab) as Transform;
                Vector2 gridPos = new Vector2(x, y);
                hex.position = CalcWorldPos(gridPos);
                hex.parent = this.transform;*/
            }
        }
        
    }

    /*private void AddGap()
    {
        hexWidth += hexWidth * gap;
        hexHeight += hexHeight * gap;
    }

    private void CalcStartPos()
    {
        float offset = 0;
        if(yDim / 2 % 2 != 0)
        {
            offset = hexWidth / 2;
        }
        float x = -hexWidth * (xDim / 2) - offset;
        float z = hexHeight * 0.75f * (yDim / 2);

        startpos = new Vector3(x, 0, z);
    }

    private Vector3 CalcWorldPos(Vector2 gridPos)
    {
        float offset = 0;
        if(gridPos.y % 2 != 0)
        {
            offset = hexWidth / 2;
        }
        float x = startpos.x + gridPos.x * hexWidth + offset;
        float z = startpos.z - gridPos.y * hexHeight * 0.75f;

        return new Vector3(x, 0, z);
    }*/

    // Update is called once per frame
    void Update()
    {
        
    }
}
