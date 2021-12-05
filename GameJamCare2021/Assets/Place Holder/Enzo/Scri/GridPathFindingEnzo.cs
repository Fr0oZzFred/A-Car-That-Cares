﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPathFindingEnzo : MonoBehaviour
{
    public GameObject cellPrefab;
    public Vector2Int sizeGrid;
    public CellEnzo[,] grid; //doit pas etre public mais on avait pas le temps

    public Material basic, visited, chosen;

    void Awake()
    {
        grid = new CellEnzo[sizeGrid.x, sizeGrid.y];
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
        for (int y = 0; y < sizeGrid.y; y++)
        {
            for (int x = 0; x < sizeGrid.x; x++)
            {
                CellEnzo c = grid[x, y];
                c.neighbors = new List<CellEnzo>();
                if (x > 0) c.neighbors.Add(grid[x - 1, y]);
                if (y > 0) c.neighbors.Add(grid[x, y - 1]);
                if (x < sizeGrid.x - 1) c.neighbors.Add(grid[x + 1, y]);
                if (y < sizeGrid.y - 1) c.neighbors.Add(grid[x, y + 1]);

                c.SetWall(Random.value < 0.2); //permet de set la proba a 20%
            }
        }
        grid[0, 0].SetWall(false);
        grid[sizeGrid.x - 1, sizeGrid.y - 1].SetWall(false);
        PathFind(grid[0, 0], grid[sizeGrid.x - 1, sizeGrid.y - 1]);
    }

    void ResetGrid()
    {
        for (int y = 0; y < sizeGrid.y; y++)
        {
            for (int x = 0; x < sizeGrid.x; x++)
            {
                CellEnzo c = grid[x, y];
                c.visited = false;
                c.SetMaterial(basic);
                c.node = null;
                c.parent = null;
            }
        }
    }

    public List<CellEnzo> PathFind(CellEnzo start, CellEnzo target)
    {
        ResetGrid();
        PriorityHeapEnzo<CellEnzo> frontier = new PriorityHeapEnzo<CellEnzo>(); //Frontier = frontier des trucs a parcourir
        start.node = frontier.Insert(start, 0);
        while (!frontier.IsEmpty())
        {
            NodeEnzo<CellEnzo> current = frontier.PopMin();
            CellEnzo cell = current.content;
            cell.visited = true;
            cell.SetMaterial(visited);
            if (cell == target) break;

            foreach (CellEnzo neigh in cell.neighbors)
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

        List<CellEnzo> res = new List<CellEnzo>();
        CellEnzo currentCell = target;
        while (currentCell.parent != null)
        {
            res.Add(currentCell);
            currentCell.SetMaterial(chosen);
            currentCell = currentCell.parent;
            if (currentCell.parent == null)
            {
                currentCell.SetMaterial(chosen);
            }
        }
        res.Reverse();
        return res; //note pour a pathfinding on fait current.priority + 1 + distance a vol d'oiseau
    }
}