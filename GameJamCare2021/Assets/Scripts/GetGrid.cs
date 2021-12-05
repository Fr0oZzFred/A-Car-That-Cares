using System.Collections.Generic;
using UnityEngine;

public class GetGrid : MonoBehaviour {
    [SerializeField]
    List<GameObject> goX;
    List<GameObject> goZ; //Child de Grid
    int indexZMax = 0; //Récup la ranger la plus grande
    GameObject[,] grid;
    private void Start() {
        //récup GO
        goZ = new List<GameObject>();
        for (int x = 0; x < goX.Count; x++) {
            for (int z = 1; z <= goX[x].transform.childCount; z++) {
                goZ.Add(goX[x].transform.Find(z.ToString()).gameObject);
            }
            if (goX[x].transform.childCount > indexZMax) indexZMax = goX[x].transform.childCount;
        }

        //Conversion en array[,] attention à changer le comblage des cases vides
        grid = new GameObject[goX.Count, indexZMax + 1];
        for (int x = 0; x < grid.GetLength(0); x++) {
            grid[x, 0] = goX[x];
            for (int z = 0; z < grid.GetLength(1) - 1; z++) {
                if (goX[x].transform.childCount > z && goZ.Count > 0) {
                    grid[x, z + 1] = goZ[z]; //Donner les GO actifs
                } else {
                    grid[x, z + 1] = new GameObject(); // à changer
                }
            }
            goZ.RemoveRange(0, goX[x].transform.childCount);
        }
    }
}
