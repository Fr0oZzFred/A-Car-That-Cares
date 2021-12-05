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
        for (int x = 0; x < sizeGrid.x; x++){
            for (int y = 0; y < sizeGrid.y; y++){
                map[x, y] = cells[y + x * sizeGrid.y];
                map[x, y].name = "Cell (" + x + ", " + y + ")";
            }
        }
        for (int y = 0; y < sizeGrid.y; y++){
            for (int x = 0; x < sizeGrid.x; x++){
                Cell c = map[x, y];
                c.neighbors = new List<Cell>();
                if (x < sizeGrid.x - 1) c.neighbors.Add(map[x + 1, y]);
                if (x > 0)              c.neighbors.Add(map[x - 1, y]);
                if (y < sizeGrid.y - 1) c.neighbors.Add(map[x, y + 1]);
                if (y > 0)              c.neighbors.Add(map[x, y - 1]);
            }
        }
    }
    public List<Cell> PathFind(Cell start, Cell target)
    {
        ResetMap();
        Debug.Log(start.name + " to " + target.name);
        PriorityHeap<Cell> frontier = new PriorityHeap<Cell>();
        start.node = frontier.Insert(start, 0);
        while (!frontier.IsEmpty()){
            Node<Cell> current = frontier.PopMin();
            Cell cell = current.content;
            cell.visited = true;
            cell.SetTrail();
            if (cell == target) break;
            foreach (Cell neigh in current.content.neighbors){
                if (neigh.visited) continue;
                if (neigh.house) continue;
                if (neigh.car) continue;
                if (neigh.eventRoad) continue;
                Debug.Log(neigh.eventRoad);
                if (neigh.node == null){
                    neigh.node = frontier.Insert(neigh, current.priority + 1);
                    neigh.parent = cell;
                }
                else if (neigh.node.priority > current.priority + 1){
                    frontier.ChangePriority(neigh.node, current.priority + 1);
                    neigh.parent = cell;
                }
            }
        }
        List<Cell> res = new List<Cell>();
        Cell currentCell = target;
        while (currentCell.parent != null)
        {
            res.Add(currentCell);
            currentCell = currentCell.parent;
        }
        res.Reverse();
        ResetMap();
        return res;
    }
    void ResetMap(){
        for (int y = 0; y < sizeGrid.y; y++){
            for (int x = 0; x < sizeGrid.x; x++){
                Cell c = map[x, y];
                c.visited = false;
                c.RemoveTrail();
                c.node = null;
                c.parent = null;
            }
        }
    }
}
