using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MapPathFinding : MonoBehaviour{
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
        for (int y = 0; y < sizeGrid.y; y++)
        {
            for (int x = 0; x < sizeGrid.x; x++)
            {
                Cell c = map[x, y];
                c.neighbors = new List<Cell>();
                if (x < sizeGrid.x - 1) c.neighbors.Add(map[x + 1, y]);
                if (x > 0) c.neighbors.Add(map[x - 1, y]);
                if (y < sizeGrid.y - 1) c.neighbors.Add(map[x, y + 1]);
                if (y > 0) c.neighbors.Add(map[x, y - 1]);
            }
        }
    }
}
