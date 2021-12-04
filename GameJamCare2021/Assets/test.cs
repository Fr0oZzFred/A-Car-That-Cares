using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public int indexZ;
    public List<GameObject> goZ;

    private void Start() {
        goZ = new List<GameObject>();
        int tutu = goZ[0].transform.childCount;
        for(int x = 0; x < goZ.Count; x++) {
            for(int z = 0; z < goZ[x].transform.childCount; z++) {
                //goz[i,z] = gameobbject.Find();
            }
        }
    }
}
