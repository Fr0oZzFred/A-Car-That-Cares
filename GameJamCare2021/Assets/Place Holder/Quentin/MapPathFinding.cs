using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MapPathFinding : MonoBehaviour{
    public GameObject bloc;
    public Vector2Int sizeGrid;
    public Cell[,] map;
    public List<Cell> cells = new List<Cell>();
    void Awake()
    {
        map = new Cell[sizeGrid.x, sizeGrid.y];
        for (int i = 0; i < sizeGrid.y; i++)
        {
            for (int j = 0; j < sizeGrid.x; j++)
            {
                map[j, i] = cells[j + i * sizeGrid.x];
            }
        }
    }
}
