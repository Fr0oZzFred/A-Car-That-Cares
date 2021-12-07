using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MapPathFinding : MonoBehaviour{
    public Vector2Int sizeGrid;
    public CellQuentin[,] map;
    public List<CellQuentin> cells = new List<CellQuentin>();
    void Awake()
    {
        map = new CellQuentin[sizeGrid.x, sizeGrid.y];
        for (int x = 0; x < sizeGrid.x; x++){
            for (int y = 0; y < sizeGrid.y; y++){
                map[x, y] = cells[y + x * sizeGrid.y];
                map[x, y].name = "Cell (" + x + ", " + y + ")";
            }
        }
        for (int y = 0; y < sizeGrid.y; y++){
            for (int x = 0; x < sizeGrid.x; x++){
                CellQuentin c = map[x, y];
                c.neighbors = new List<CellQuentin>();
                if (x < sizeGrid.x - 1) c.neighbors.Add(map[x + 1, y]);
                if (x > 0)              c.neighbors.Add(map[x - 1, y]);
                if (y < sizeGrid.y - 1) c.neighbors.Add(map[x, y + 1]);
                if (y > 0)              c.neighbors.Add(map[x, y - 1]);
            }
        }
    }
    public List<CellQuentin> PathFind(CellQuentin start, CellQuentin target)
    {
        ResetMap();
        Debug.Log(start.name + " to " + target.name);
        PriorityHeapQuentin<CellQuentin> frontier = new PriorityHeapQuentin<CellQuentin>();
        start.node = frontier.Insert(start, 0);
        while (!frontier.IsEmpty()){
            NodeQuentin<CellQuentin> current = frontier.PopMin();
            CellQuentin cell = current.content;
            cell.visited = true;
            cell.SetTrail();
            if (cell == target) break;
            foreach (CellQuentin neigh in current.content.neighbors){
                if (neigh.visited) continue;
                if (neigh.house) continue;
                if (neigh.car) continue;
                if (neigh.eventRoad) continue;
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
        List<CellQuentin> res = new List<CellQuentin>();
        CellQuentin currentCell = target;
        while (currentCell.parent != null)
        {
            res.Add(currentCell);
            currentCell = currentCell.parent;
        }
        res.Reverse();
        string way = "chemin" + start.name;
        for(int i = 0; i < res.Count; i++)
        {
            way += " -> " + res[i];
        }
        Debug.Log(way);
        ResetMap();
        return res;
    }
    void ResetMap(){
        for (int y = 0; y < sizeGrid.y; y++){
            for (int x = 0; x < sizeGrid.x; x++){
                CellQuentin c = map[x, y];
                c.visited = false;
                c.RemoveTrail();
                c.node = null;
                c.parent = null;
            }
        }
    }
}
