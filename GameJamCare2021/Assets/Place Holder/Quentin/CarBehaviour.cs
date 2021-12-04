using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CarBehaviour : MonoBehaviour
{
    public MapPathFinding map;
    public static CarBehaviour globalCar;
    void Awake(){
        globalCar = this;    
    }
    public void GoTo(Cell target){
    }
}
