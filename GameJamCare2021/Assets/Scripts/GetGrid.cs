using System.Collections.Generic;
using UnityEngine;

public class GetGrid : MonoBehaviour {
    [SerializeField]
    List<GameObject> goX;
    List<int> goXindex;
    List<GameObject> goZ; //Child de Grid
    int indexZMax = 0; //Récup la ranger la plus grande
    public static CellEnzo[,] grid { get; private set; }
    GameObject wall;
    private void Start() {
        wall = new GameObject();
        wall.AddComponent<CellEnzo>();
        goZ = new List<GameObject>();
        goXindex = new List<int>();
        for (int x = 0; x < goX.Count; x++) {
            int z = 1;
            foreach(Transform go in goX[x].transform) {
                if(go.gameObject.name == z.ToString()) {
                    goZ.Add(go.gameObject);
                    z++;
                }
            }
            goXindex.Add(z-1);
                /*for (int z = 1; z <= goX[x].transform.childCount; z++) {
                goZ.Add(goX[x].transform.Find(z.ToString()).gameObject);
            }*/
            if (z-1 > indexZMax) indexZMax = z-1;
        }
        //Converttion en array[,] attention à changer le comblage des cases vides
        grid = new CellEnzo[goX.Count, indexZMax + 1];
        for (int x = 0; x < grid.GetLength(0); x++) {
            grid[x, 0] = goX[x].GetComponent<CellEnzo>();
            for (int z = 0; z < grid.GetLength(1) - 1; z++) {
                if (goXindex[x] > z && goZ.Count > 0) {
                    grid[x, z + 1] = goZ[z].GetComponent<CellEnzo>();
                } else {
                        grid[x, z + 1] = wall.GetComponent<CellEnzo>(); ; // à changer
                    }
            }
            goZ.RemoveRange(0, goXindex[x]);
        }
        for (int x = 0; x < grid.GetLength(0); x++) {
            for (int z = 0; z < grid.GetLength(1); z++) {
                //Debug.Log(grid[x, z], grid[x, z]);
            }
        }
    }
}
