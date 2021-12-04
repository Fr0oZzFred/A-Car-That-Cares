using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    int indexZ;
    //Debug.Log(transform.childCount); childCount ne prend en compte les enfants d'un enfant
    public List<GameObject> goZ;
    int[] tutu;
    private void Start() {
        /*goZ = new List<GameObject>();
        int tutu = goZ[0].transform.childCount;
        for(int x = 0; x < goZ.Count; x++) {
            for(int z = 0; z < goZ[x].transform.childCount; z++) {
                //goz[i,z] = gameobbject.Find($"Cube{Z}"); Soucis avec les noms, nomeclature chiant?
            }
        }*/
        //ToArray() est suffisant pour Quentin? ou fonctione spécial pour lui?
    }
}
