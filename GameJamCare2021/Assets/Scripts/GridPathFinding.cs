using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPathFinding : MonoBehaviour
{
    public GameObject cellPrefab;
    Vector2Int sizeGrid;
    public Cell[,] grid;
    public Material chosen;
    //public Material basic, visited, chosen;

    void Start() //Awake
    {
        grid = GetGrid.grid;
        sizeGrid.x = GetGrid.grid.GetLength(0);
        sizeGrid.y = GetGrid.grid.GetLength(1);
        /*
        for (int y = 0; y < sizeGrid.y; y++)
        {
            for (int x = 0; x < sizeGrid.x; x++)
            {
                GameObject go = Instantiate(cellPrefab);
                go.name = "Cell [" + x + ";" + y + "]";
                go.transform.position = new Vector3(x * 2, 0, y * 2); //go.transform.position = new Vector3(x * 1.1, 0, y *1.1); PERMET ASPECT GRID
                grid[x, y] = go.GetComponent<CellEnzo>();
            }
        }
        */
        for (int y = 0; y < sizeGrid.y; y++)
        {
            for (int x = 0; x < sizeGrid.x; x++)
            {
                Cell c = grid[x, y];
                c.neighbors = new List<Cell>();
                if (x > 0) c.neighbors.Add(grid[x - 1, y]);
                if (y > 0) c.neighbors.Add(grid[x, y - 1]);
                if (x < sizeGrid.x - 1) c.neighbors.Add(grid[x + 1, y]);
                if (y < sizeGrid.y - 1) c.neighbors.Add(grid[x, y + 1]);

                //c.SetWall(Random.value < 0.2); //permet de set la proba a 20%
            }
        }
        //grid[0, 0].SetWall(false);
        //grid[sizeGrid.x - 1, sizeGrid.y - 1].SetWall(false);
        PathFind(grid[0, 0], grid[sizeGrid.x - 1, sizeGrid.y - 1]);
    }

    void ResetGrid()
    {
        for (int y = 0; y < sizeGrid.y; y++)
        {
            for (int x = 0; x < sizeGrid.x; x++)
            {
                Cell c = GetGrid.grid[x, y];
                c.visited = false;
                c.SetMaterial(chosen, false,true);
                c.node = null;
                c.parent = null;
            }
        }
    }

    public List<Cell> PathFind(Cell start, Cell target)
    {
        ResetGrid();
        PriorityHeap<Cell> frontier = new PriorityHeap<Cell>(); //Frontier = frontier des trucs a parcourir
        start.node = frontier.Insert(start, 0);
        while (!frontier.IsEmpty())
        {
            Node<Cell> current = frontier.PopMin();
            Cell cell = current.content;
            cell.visited = true;
            //cell.SetMaterial(visited);
            if (cell == target) break;

            foreach (Cell neigh in cell.neighbors)
            {
                if (neigh.visited) continue;
                if (neigh.IsWall) continue;
                if (neigh.IsBlock) continue;

                if (neigh.node == null)
                {
                    neigh.node = frontier.Insert(neigh, current.priority + 1);
                    neigh.parent = cell;
                }
                else if (neigh.node.priority > current.priority + 1)  //Sert pour changer le chemin si on trouve plus court
                {
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
            currentCell.SetMaterial(chosen,true,false);
            currentCell = currentCell.parent;
            if (currentCell.parent == null)
            {
                currentCell.SetMaterial(chosen,false,false);
            }
        }
        target.SetMaterial(chosen, true, true);
        res.Reverse();
        return res; //note pour a pathfinding on fait current.priority + 1 + distance a vol d'oiseau
    }

    public List<Cell> PathFindMenu(Cell start, Cell target)
    {
        ResetGrid();
        PriorityHeap<Cell> frontier = new PriorityHeap<Cell>(); //Frontier = frontier des trucs a parcourir
        start.node = frontier.Insert(start, 0);
        while (!frontier.IsEmpty())
        {
            Node<Cell> current = frontier.PopMin();
            Cell cell = current.content;
            cell.visited = true;
            //cell.SetMaterial(visited);
            if (cell == target) break;

            foreach (Cell neigh in cell.neighbors)
            {
                if (neigh.visited) continue;
                if (neigh.IsWall) continue;

                if (neigh.node == null)
                {
                    neigh.node = frontier.Insert(neigh, current.priority + 1);
                    neigh.parent = cell;
                }
                else if (neigh.node.priority > current.priority + 1)  //Sert pour changer le chemin si on trouve plus court
                {
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
            //currentCell.SetMaterial(chosen, true, false);
            currentCell = currentCell.parent;
            if (currentCell.parent == null)
            {
                //currentCell.SetMaterial(chosen, false, false);
            }
        }
        //target.SetMaterial(chosen, true, true);
        res.Reverse();
        return res; //note pour a pathfinding on fait current.priority + 1 + distance a vol d'oiseau
    }
}
