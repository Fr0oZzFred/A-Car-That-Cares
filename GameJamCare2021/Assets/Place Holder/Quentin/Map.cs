using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Map : MonoBehaviour {
    public int x, y;
    public Cell[,] map;
    public List<Cell> cells = new List<Cell>();
    void Awake()
    {
        map = new Cell[x, y];
        for(int i = 0; i < y; i++){
            for(int j = 0; j < x; x++){
                map[j, i] = cells[j + i * x];
            }
        }
    }
}
